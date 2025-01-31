﻿#define DEBUG_STRIP
#define DEBUG_RESTORE_BACKUP
#define DEBUG_UNPACK_PREFAB
#define DEBUG_CREATE_BACKUP

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Build;
using JetBrains.Annotations;
using Sisus.HierarchyFolders.Prefabs;

namespace Sisus.HierarchyFolders
{
	[InitializeOnLoad]
	internal class HierarchyFolderBuildPrefabRemover : IPostprocessBuildWithReport
	{
		private static List<KeyValuePair<string, string>> paths = null;
		private static bool warnedAboutRemoveFromBuildDisabled;

		private static bool PrefabsStripped
		{
			get
			{
				return EditorPrefs.GetBool("HF.PrefabsStripped", false);
			}

			set
			{
				if(value)
				{
					EditorPrefs.SetBool("HF.PrefabsStripped", true);
				}
				else
				{
					EditorPrefs.DeleteKey("HF.PrefabsStripped");
				}
			}
		}

		public int callbackOrder
		{
			get
			{
				return 1000;
			}
		}

		static HierarchyFolderBuildPrefabRemover()
		{
			if(!PrefabsStripped || BuildPipeline.isBuildingPlayer)
			{
				return;
			}

			RestoreBackups();
		}

		[PostProcessScene(0), UsedImplicitly]
		private static void OnPostProcessScene()
		{
			// This will also get called when entering Playmode, when SceneManager.LoadScene is called,
			// but we only want to do stripping just after building the Scene.
			if(Application.isPlaying)
			{
				return;
			}

			if(PrefabsStripped)
			{
				return;
			}

			var preferences = HierarchyFolderPreferences.Get();
			if(preferences == null)
			{
				Debug.LogWarning("Failed to find Hierarchy Folder Preferences asset; will not strip hierarchy folders from build.");
				return;
			}

			StrippingType strippingType;

			switch(preferences.foldersInPrefabs)
			{
				case HierachyFoldersInPrefabs.NotAllowed:
					// No need to do any build stripping if no hierarchy folders are allowed to exist in prefabs.
					return;
				case HierachyFoldersInPrefabs.StrippedAtRuntime:
					// No need to do any build stripping if stripping occurs at runtime only.
					return;
				case HierachyFoldersInPrefabs.NotStripped:
					if(!preferences.warnWhenNotRemovedFromBuild || warnedAboutRemoveFromBuildDisabled || EditorUtility.DisplayDialog("Warning: Hierarchy Folder Prefab Stripping Disabled", "This is a reminder that you have disabled stripping of hierarchy folders from prefabs from builds. If you have any hierarchy folders inside prefabs this can result in suboptimal performance.", "Continue Anyway", "Enable Stripping"))
					{
						warnedAboutRemoveFromBuildDisabled = preferences.warnWhenNotRemovedFromBuild;

						// When value is NotStripped we need to remove or disable all HierarchyFolders in prefabs to ensure that runtime stripping does not take place.
						strippingType = StrippingType.RemoveComponent;
						break;
					}

					preferences.foldersInPrefabs = HierachyFoldersInPrefabs.StrippedAtBuildTime;
					strippingType = StrippingType.FlattenHierarchyAndRemoveGameObject;
					break;
				case HierachyFoldersInPrefabs.StrippedAtBuildTime:
					strippingType = StrippingType.FlattenHierarchyAndRemoveGameObject;
					break;
				default:
					Debug.LogWarning("Unrecognized HierachyFoldersInPrefabs value: " + preferences.foldersInPrefabs);
					return;
			}
			
			string backupRootDir = GetBackupRootDirectory();
			if(Directory.Exists(backupRootDir) && Directory.GetFiles(backupRootDir).Length > 0)
			{
				EditorUtility.DisplayDialog("Existing Prefab Backups Detected", "Unable to strip hierarchy folders from prefabs for the build, because If you have any hierarchy folders inside prefabs this will result in suboptimal performance and is not recommended when making a release build.", "Continue Anyway", "Enable Stripping");
				throw new BuildFailedException($"Can not perform stripping of hierarchy folders from prefabs, because prefab backup directory '{backupRootDir}' already contains files.\nPlease");
			}

			if(CreateBackups())
			{
				StripHierarchyFoldersFromAllPrefabs(strippingType);
				EditorApplication.delayCall += RestoreBackupsAfterBuildHasFinished;
			}

			PrefabsStripped = true;
		}

		public void OnPostprocessBuild(BuildReport report)
		{
			if(!PrefabsStripped)
			{
				return;
			}

			RestoreBackups();

			PrefabsStripped = false;
		}

		public static bool CreateBackups()
		{
			paths = new List<KeyValuePair<string, string>>();

			bool success = true;

			string backupRootDir = GetBackupRootDirectory();

			var assets = AssetDatabase.FindAssets("t:GameObject");
			foreach(var guid in assets)
			{
				var assetPath = AssetDatabase.GUIDToAssetPath(guid);
				var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
				if(gameObject == null)
				{
					continue;
				}
				var o = gameObject.GetComponentInChildren<HierarchyFolder>(true);
				if(o == null)
				{
					continue;
				}

				// Risk of path being too long to write to???
				string backupPath = Path.Combine(backupRootDir, assetPath);

				paths.Add(new KeyValuePair<string, string>(assetPath, backupPath));

				Directory.CreateDirectory(Path.GetDirectoryName(backupPath));

				#if DEV_MODE && DEBUG_CREATE_BACKUP
				Debug.Log("Creating backup of prefab " + backupPath + "\n@ " + backupPath);
				#endif

				try
				{
					File.Copy(assetPath, backupPath, true);
				}
				catch(Exception e)
				{
					Debug.LogError("Failed to create prefab backup from " + assetPath + " to " + backupPath + ". Can not perform stripping.\n" + e);
					success = false;
				}
			}

			return success;
		}

		// Generate a unique Append a unique hash Application.persistentDataPath
		private static string GetBackupRootDirectory() => Path.Combine(Application.persistentDataPath, "HierarchyFolders/PrefabBackups", Hash128.Compute(Application.dataPath).ToString());

		private static void StripHierarchyFoldersFromAllPrefabs(StrippingType strippingType)
		{
			if(paths != null)
			{
				foreach(var assetAndBackupPath in paths)
				{
					var assetPath = assetAndBackupPath.Key;
					var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
					StripPrefab(gameObject, strippingType);
				}
				return;
			}

			paths = new List<KeyValuePair<string, string>>();

			string backupRootDirectory = GetBackupRootDirectory();
			var assets = AssetDatabase.FindAssets("t:GameObject");
			foreach(var guid in assets)
			{
				var assetPath = AssetDatabase.GUIDToAssetPath(guid);
				var gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
				if(gameObject == null)
				{
					continue;
				}
				var o = gameObject.GetComponentInChildren<HierarchyFolder>(true);
				if(o == null)
				{
					continue;
				}

				// Risk of path being too long to write to???
				string backupPath = Path.Combine(backupRootDirectory, assetPath);

				#if DEV_MODE && DEBUG_STRIP
				Debug.Log("Stripping prefab " + assetPath);
				#endif

				paths.Add(new KeyValuePair<string, string>(assetPath, backupPath));

				StripPrefab(gameObject, strippingType);
			}
		}

		private static void StripPrefab(GameObject root, StrippingType strippingType)
		{
			string assetPath = AssetDatabase.GetAssetPath(root);
			using(var scope = new PrefabUtility.EditPrefabContentsScope(assetPath))
			{
				var transform = scope.prefabContentsRoot.transform;

				if(transform.gameObject.IsConnectedPrefabInstance())
				{
					#if DEV_MODE && DEBUG_UNPACK_PREFAB
					Debug.Log("Unpacking GameObject " + transform.name + " on asset "+ PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(transform.gameObject) + " with IsPrefabAsset=" + transform.gameObject.IsPrefabAsset() + ", IsConnectedPrefabInstance=" + transform.gameObject.IsConnectedPrefabInstance(), transform.root);
					#endif

					PrefabUtility.UnpackPrefabInstance(transform.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
				}

				int childCount = transform.childCount;
				var children = new Transform[childCount];
				for(int n = 0; n < childCount; n++)
				{
					children[n] = transform.GetChild(n);
				}
				for(int n = 0; n < childCount; n++)
				{
					HierarchyFolderUtility.CheckForAndRemoveHierarchyFoldersInChildren(children[n], strippingType, true);
				}

			}
		}

		private static void RestoreBackupsAfterBuildHasFinished()
        {
			if(BuildPipeline.isBuildingPlayer)
			{
				EditorApplication.delayCall += RestoreBackupsAfterBuildHasFinished;
				return;
			}
			EditorApplication.delayCall += RestoreBackups;
		}

		private static void RestoreBackups()
        {
			RestoreBackups(deleteAfterRestored: true);
		}

		private static void RestoreBackups(bool deleteAfterRestored)
		{
			if(paths != null)
			{
				if(paths.Count == 0)
				{
					return;
				}

				AssetDatabase.StartAssetEditing();

				foreach(var assetAndBackupPath in paths)
				{
					var assetPath = assetAndBackupPath.Key;
					var backupPath = assetAndBackupPath.Value;

					#if DEV_MODE && DEBUG_RESTORE_BACKUP
					Debug.Log("Restoring backup of prefab " + assetPath + "\nfrom " + backupPath);
					#endif

					try
					{
						File.Copy(backupPath, assetPath, true);
					}
					catch(Exception e)
                    {
						Debug.LogError("Failed to restore prefab backup from " + backupPath + " to " + assetPath + "! " + e);
						continue;
                    }

					if(deleteAfterRestored)
					{
						try
						{
							File.Delete(backupPath);
						}
						catch(Exception e)
						{
							Debug.LogWarning("Failed to delete already restored backup of asset " + assetPath + " at " + backupPath + ". " + e);
						}
					}
				}
				paths.Clear();
				AssetDatabase.StopAssetEditing();
				AssetDatabase.Refresh();
				return;
			}

			string backupRootDir = GetBackupRootDirectory();
			int removePrefixLength = backupRootDir.Length + 1;
			if(!Directory.Exists(backupRootDir))
			{
				return;
			}

			var backupPaths = Directory.GetFiles(backupRootDir, "*.prefab", SearchOption.AllDirectories);

			if(backupPaths.Length == 0)
            {
				return;
            }

			AssetDatabase.StartAssetEditing();

			foreach(var backupPath in backupPaths)
			{
				string assetPath = backupPath.Substring(removePrefixLength);

				#if DEV_MODE && DEBUG_RESTORE_BACKUP
				Debug.Log("Restoring backup of prefab " + assetPath + "\nfrom " + backupPath);
				#endif

				try
				{
					File.Copy(backupPath, assetPath, true);
				}
				catch(Exception e)
                {
					Debug.LogError("Failed to restore prefab backup from " + backupPath + " to " + assetPath + "! " + e);
					continue;
                }

				if(deleteAfterRestored)
				{
					try
					{
						File.Delete(backupPath);
					}
					catch(Exception e)
					{
						Debug.LogWarning("Failed to delete already restored backup of asset " + assetPath + " at " + backupPath + ". " + e);
					}
				}
			}

			AssetDatabase.StopAssetEditing();
			AssetDatabase.Refresh();
		}
	}
}