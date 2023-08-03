using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class FindMissingScripts 
{
    // Start is called before the first frame update
    [MenuItem("My menu/Find missing scripts")]
    static void FindMissingScriptsInProject()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
            .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();
        foreach (string path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            foreach (var component in prefab.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.Log("prefab found with missing script: " + path, prefab);
                }
            }
        }
    }
}
