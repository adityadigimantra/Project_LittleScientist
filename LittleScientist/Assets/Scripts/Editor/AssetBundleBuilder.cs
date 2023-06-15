using System;
using UnityEngine;
using UnityEditor;


public class AssetBundleBuilder
{

    [MenuItem("Assets/CreateAssetsBundles")]
    public static void BuildAllAssetBundles()
    {
        string assetBundleDirectoryPath = Application.dataPath + "/../AssetsBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch(Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}
