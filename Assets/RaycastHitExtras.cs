using UnityEngine;

public class RaycastHitExtras : MonoBehaviour
{
    public void DisableRaycastHitObject(RaycastHit hit)
    {
        Debug.Log(hit);
        hit.collider.gameObject.SetActive(false);
    }
}