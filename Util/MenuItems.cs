#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MijanTools.Util
{
    public static class MenuItems
    {
        [MenuItem("Mijan Tools/Player Prefs/Delete All")]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Mijan Tools/Application Data Path/Print to Console")]
        public static void LogApplicationDataPath()
        {
            Debug.Log(Application.persistentDataPath);
        }

        [MenuItem("Mijan Tools/Application Data Path/Open Folder")]
        public static void OpenApplicationDataPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }
}
#endif