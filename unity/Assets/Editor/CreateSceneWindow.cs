using UnityEditor;
using UnityEngine;

public class CreateSceneWindow : EditorWindow
{
    private string _sceneKey = "";
    private string _location;

    public SerializableDateTime Time = new SerializableDateTime(2095, 08, 7, 11, 0, 0);


    public void OnGUI()
    {
        GUILayout.Label("Create a Scene", EditorStyles.boldLabel);

        _sceneKey = EditorGUILayout.TextField("Key", _sceneKey);
        _location = EditorGUILayout.TextField("Location", _location);

        SerializedObject so = new SerializedObject(this);
        SerializedProperty startTimeProperty = so.FindProperty("Time");
        
        EditorGUILayout.PropertyField(startTimeProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties

        if(GUILayout.Button("Create!"))
        {
            CreateScene();
            Close();
        }
    }

    private void CreateScene()
    {
        SceneData sceneData = CreateInstance<SceneData>();
        sceneData.Key = _sceneKey;
        sceneData.Name = _sceneKey;
        sceneData.Location = _location;
        sceneData.Time = Time;

        string assetName = string.Format("{0}.asset", sceneData.Key.ToLowerInvariant());

        AssetDatabase.CreateAsset(sceneData, string.Format("{0}{1}", ResourcePaths.StoryEngineSceneFullPath, assetName));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}