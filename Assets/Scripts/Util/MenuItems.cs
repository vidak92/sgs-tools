#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace SGSTools.Util
{
    public static class MenuItems
    {
        [MenuItem("SGS Tools/Player Prefs/Delete All")]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("SGS Tools/Application Data Path/Print to Console")]
        public static void LogApplicationDataPath()
        {
            Debug.Log(Application.persistentDataPath);
        }

        [MenuItem("SGS Tools/Application Data Path/Open Folder")]
        public static void OpenApplicationDataPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        [MenuItem("SGS Tools/Take Screenshot")]
        public static void TakeScreenshot()
        {
            // @TODO extract method to EditorUtils
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss");
            var directoryPath = $"{Application.persistentDataPath}/screenshots";
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }
            var filePath = $"{directoryPath}/{timestamp}.png";
            ScreenCapture.CaptureScreenshot(filePath);
            Debug.Log($"Screenshot saved at: {filePath}");
        }
    }
}
#endif