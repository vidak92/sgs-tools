#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace SGSTools.Util
{
    public static class EditorUtils
    {
#if UNITY_2019
        public static void DrawEditableList(SerializedProperty list)
        {
            EditorGUILayout.LabelField(list.displayName);
            if (list.isExpanded)
            {
                for (int i = 0; i < list.arraySize; i++)
                {
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
                    EditorGUILayout.Separator();
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button(new GUIContent("\u2191", "move up"), EditorStyles.miniButtonLeft))
                    {
                        list.MoveArrayElement(i, i - 1);
                    }
                    if (GUILayout.Button(new GUIContent("\u2193", "move down"), EditorStyles.miniButtonRight))
                    {
                        list.MoveArrayElement(i, i + 1);
                    }
                    if (GUILayout.Button(new GUIContent("+", "add"), EditorStyles.miniButtonLeft))
                    {
                        list.InsertArrayElementAtIndex(i);
                    }
                    if (GUILayout.Button(new GUIContent("-", "remove"), EditorStyles.miniButtonRight))
                    {
                        list.DeleteArrayElementAtIndex(i);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Separator();
                }
            }
            EditorGUILayout.Separator();
        }
#endif
        
        public static bool CreateAssetFolderPath(string path)
        {
            if (AssetDatabase.AssetPathExists(path))
            {
                // @TODO log
                return true;
            }

            var assetPathIndex = path.IndexOf("Assets/", StringComparison.InvariantCulture);
            if (assetPathIndex < 0)
            {
                // @TODO log error
                return false;
            }
            var startPathIndex =  + assetPathIndex + "Assets/".Length;
            var folderPath = path.Substring(0, startPathIndex - 1);
            var subfolders = path.Substring(startPathIndex).Split("/");
            for (var i = 0; i < subfolders.Length; i++)
            {
                var subfolder = subfolders[i];
                var subfolderPath = $"{folderPath}/{subfolder}";
                if (!AssetDatabase.AssetPathExists(subfolderPath))
                {
                    var guid = AssetDatabase.CreateFolder(folderPath, subfolder);
                    if (string.IsNullOrEmpty(guid))
                    {
                        // @TODO log error
                        return false;
                    }
                }

                folderPath = subfolderPath;
            }

            return true;
        }

        public static bool CreateAssetIncludingFolderPath(Object asset, string folderPath, string assetName)
        {
            var assetPath = $"{folderPath}/{assetName}";
            if (AssetDatabase.AssetPathExists(assetPath))
            {
                // @TODO add overwrite param
                Debug.LogWarning($"SGSTools: Asset already exists at {assetPath}");
                return true;
            }
            
            var success = CreateAssetFolderPath(folderPath);
            if (!success)
            {
                Debug.LogError($"SGSTools: Couldn't create new folder for asset at {folderPath}");
                return false;
            }
            
            AssetDatabase.CreateAsset(asset, assetPath);
            // @TODO check if asset was successfully created?
            // @TODO add save assets & refresh param?
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return true;
        }
    }
}
#endif