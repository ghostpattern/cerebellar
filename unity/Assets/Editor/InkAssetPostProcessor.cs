using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class InkAssetPostProcessor : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAssets,
         string[] deletedAssets,
         string[] movedAssets,
         string[] movedFromAssetPaths)
    {
        bool inkFileChanged = false;
        foreach(string asset in importedAssets)
        {
            if(asset.EndsWith(".ink"))
            {
                inkFileChanged = true;
            }
        }

        if(inkFileChanged == false)
        {
            foreach(string asset in deletedAssets)
            {
                if(asset.EndsWith(".ink"))
                {
                    inkFileChanged = true;
                }
            }
        }

        if(inkFileChanged == false)
        {
            foreach(string asset in movedAssets)
            {
                if(asset.EndsWith(".ink"))
                {
                    inkFileChanged = true;
                }
            }
        }

        if(inkFileChanged == false)
        {
            foreach(string asset in movedFromAssetPaths)
            {
                if(asset.EndsWith(".ink"))
                {
                    inkFileChanged = true;
                }
            }
        }

        if(inkFileChanged)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("..\\tools\\bin\\inklecate.exe", "-o Assets\\Resources\\Story\\story.json Assets\\Stories\\root.ink")
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            Process process = Process.Start(startInfo);
            if(process != null)
            {
                string error = process.StandardError.ReadToEnd();
                if(string.IsNullOrEmpty(error) == false)
                    Debug.LogError(error);

                string output = process.StandardOutput.ReadToEnd();
                if(string.IsNullOrEmpty(output) == false)
                    Debug.Log(output);
            }
        }
    }
}
