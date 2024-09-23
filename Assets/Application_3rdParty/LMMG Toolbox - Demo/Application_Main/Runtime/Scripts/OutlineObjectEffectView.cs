using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineObjectEffectView : MonoBehaviour
{
    protected enum OutlineMode
    {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }

    [Serializable]
    protected class Vector3Data
    {
        public List<Vector3> data;
    }

    #region Variables

    #region Private Variables

    private static readonly HashSet<Mesh> RegisteredMeshes = new();
    private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
    private static readonly int ZTest = Shader.PropertyToID("_ZTest");
    private static readonly int OutlineWidth = Shader.PropertyToID("_OutlineWidth");

    #endregion
    
    #region Protected Variables

    [SerializeField, Range(0f, 10f)] protected float outlineWidth;
    [SerializeField] protected float blinkSpeed;
    [SerializeField] protected bool precomputeOutline;
    [SerializeField] protected Color outlineColor;
    [SerializeField] protected OutlineMode outlineMode;
    [SerializeField] protected List<Mesh> bakeKeys = new();
    [SerializeField] protected List<Vector3Data> bakeValues = new();

    protected float _outlineWidth;
    protected bool _needsUpdate;
    protected Renderer _meshRenderer;
    protected Material _outlineMaskMaterial, _outlineFillMaterial;

    #endregion

    #endregion

    #region Methods

    #region Private Methods

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));
        _outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
        SmoothNormals();
    }
    private void OnDisable()
    {
        StopFeedback();
    }
    private void OnDestroy()
    {
        Destroy(_outlineMaskMaterial);
        Destroy(_outlineFillMaterial);
    }


    private void UpdateMaterials()
    {
        _outlineFillMaterial.SetColor(OutlineColor, outlineColor);
        switch (outlineMode)
        {
            case OutlineMode.OutlineAll:
                _outlineMaskMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Always);
                _outlineFillMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Always);
                _outlineFillMaterial.SetFloat(OutlineWidth, _outlineWidth);
                break;
            case OutlineMode.OutlineVisible:
                _outlineMaskMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Always);
                _outlineFillMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                _outlineFillMaterial.SetFloat(OutlineWidth, _outlineWidth);
                break;
            case OutlineMode.OutlineHidden:
                _outlineMaskMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Always);
                _outlineFillMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Greater);
                _outlineFillMaterial.SetFloat(OutlineWidth, _outlineWidth);
                break;
            case OutlineMode.OutlineAndSilhouette:
                _outlineMaskMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                _outlineFillMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Always);
                _outlineFillMaterial.SetFloat(OutlineWidth, _outlineWidth);
                break;
            case OutlineMode.SilhouetteOnly:
                _outlineMaskMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Greater);
                _outlineFillMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                _outlineFillMaterial.SetFloat(OutlineWidth, _outlineWidth);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void BakeMaterials()
    {
        var bakedMeshes = new HashSet<Mesh>();
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {
            if (!bakedMeshes.Add(meshFilter.sharedMesh)) continue;

            var smoothNormals = GetSmoothNormals(meshFilter.sharedMesh);
            bakeKeys.Add(meshFilter.sharedMesh);
            bakeValues.Add(new Vector3Data() { data = smoothNormals });
        }
    }
    private void SmoothNormals()
    {
        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
        {
            if (!RegisteredMeshes.Add(meshFilter.sharedMesh)) continue;

            var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
            var smoothNormals = (index >= 0) ? bakeValues[index].data : GetSmoothNormals(meshFilter.sharedMesh);
            var meshFilterRenderer = meshFilter.GetComponent<Renderer>();

            meshFilter.sharedMesh.SetUVs(3, smoothNormals);

            if (meshFilterRenderer != null)
                CombineSubmeshes(meshFilter.sharedMesh, meshFilterRenderer.sharedMaterials);
        }

        foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (!RegisteredMeshes.Add(skinnedMeshRenderer.sharedMesh)) continue;

            var sharedMesh = skinnedMeshRenderer.sharedMesh;
            sharedMesh.uv4 = new Vector2[sharedMesh.vertexCount];
            CombineSubmeshes(sharedMesh, skinnedMeshRenderer.sharedMaterials);
        }
    }
    private static void CombineSubmeshes(Mesh mesh, Material[] materials)
    {
        if (mesh.subMeshCount == 1 || mesh.subMeshCount > materials.Length) return;

        mesh.subMeshCount++;
        mesh.SetTriangles(mesh.triangles, mesh.subMeshCount - 1);
    }
    private IEnumerator Blink()
    {
        var smoothness = 0.01f;
        var blinkDelta = (outlineWidth * smoothness) / blinkSpeed;

        while (true)
        {
            for (var i = _outlineWidth; i < outlineWidth + 0.01f; i += blinkDelta)
            {
                yield return new WaitForSeconds(smoothness);
                _outlineWidth = i;
                UpdateMaterials();
            }

            for (var i = _outlineWidth; i > 0 - 0.01f; i -= blinkDelta)
            {
                yield return new WaitForSeconds(smoothness);
                _outlineWidth = i;
                UpdateMaterials();
            }

            yield return new WaitForSeconds(smoothness);
        }
    }
    private static List<Vector3> GetSmoothNormals(Mesh mesh)
    {
        var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
            .GroupBy(pair => pair.Key);
        var smoothNormals = new List<Vector3>(mesh.normals);

        foreach (var group in groups)
        {
            var smoothNormal = Vector3.zero;

            if (group.Count() == 1) continue;
            smoothNormal = @group.Aggregate(smoothNormal, (current, pair) => current + smoothNormals[pair.Value]);
            smoothNormal.Normalize();

            foreach (var pair in group)
                smoothNormals[pair.Value] = smoothNormal;
        }

        return smoothNormals;
    }

    #endregion

    #region Public Methods

    public void StartFeedback()
    {
        var materials = _meshRenderer.sharedMaterials.ToList();
        var numberOfMaterials = materials.Count(material => material == _outlineFillMaterial || material == _outlineMaskMaterial);
        if (numberOfMaterials == 2) return;

        _outlineFillMaterial.SetFloat(OutlineWidth, _outlineWidth);
        _outlineMaskMaterial.SetFloat(OutlineWidth, _outlineWidth);

        materials.Add(_outlineMaskMaterial);
        materials.Add(_outlineFillMaterial);

        _meshRenderer.materials = materials.ToArray();

        UpdateMaterials();

        bakeKeys.Clear();
        bakeValues.Clear();
        BakeMaterials();
/*
            if (!precomputeOutline && bakeKeys.Count != 0 || bakeKeys.Count != bakeValues.Count)
            {
                bakeKeys.Clear();
                bakeValues.Clear();
            }

            if (precomputeOutline && bakeKeys.Count == 0)
            {
                BakeMaterials();
            }
*/

        StartCoroutine(Blink());
    }

    public void StopFeedback()
    {
        var materials = _meshRenderer.sharedMaterials.ToList();
        materials.Remove(_outlineMaskMaterial);
        materials.Remove(_outlineFillMaterial);
        _meshRenderer.materials = materials.ToArray();

        UpdateMaterials();
        StopAllCoroutines();

        _outlineWidth = 0;
    }

    #endregion

    #endregion
}