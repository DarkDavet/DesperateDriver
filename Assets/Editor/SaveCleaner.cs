using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveCleaner
{
    [MenuItem("Tools/Clear save files")]
    public static void ClearSaveFiles()
    {
        string path = Application.persistentDataPath;
        string[] save_files = Directory.GetFiles(path);

        if (save_files.Length == 0)
        {
            Debug.Log("There aren't any save files in the storage");
            return;
        }

        foreach (string file in save_files)
        {
            File.Delete(file);
            Debug.Log($"Deleted the file: {Path.GetFileName(file)}");
        }

        Debug.Log("<color=green>All save files are succesfully deleted!</color>");
    }
    
}
