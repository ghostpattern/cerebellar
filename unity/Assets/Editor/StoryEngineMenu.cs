using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class StoryEngineMenu : MonoBehaviour
{
    [MenuItem("StoryEngine/Create/Scene")]
    static void CreateScene()
    {
        EditorWindow.GetWindow(typeof(CreateSceneWindow));
    }

    [MenuItem("StoryEngine/Compile Ink Stories")]
    static void CompileInkStories()
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