using UnityEditor;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public class ProjectSetup
{
    static ProjectSetup()
    {
        // The directory you want to remove
        string directoryPath = "Assets/Bodynodes/body-nodes-host/body-nodes-common/cbasic"; // Change this to your directory path

        // Check if the directory exists
        if (Directory.Exists(directoryPath))
        {
            try
            {
                Directory.Delete(directoryPath, true); // true to delete recursively
                Debug.Log($"Directory {directoryPath} removed successfully on startup.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error removing directory: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"Directory {directoryPath} does not exist on startup.");
        }
    }
}
