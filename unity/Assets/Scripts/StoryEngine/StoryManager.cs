#define MULTI_TOUCH_SKIP

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class StoryValueBase
{
    public abstract Type Type { get; }
}

public class StoryValue<T> : StoryValueBase
{
    private T _value;
    public override Type Type { get { return typeof(T); } }
}

public enum GameplayTextMode
{
    Timer,
    ClickToContinue
}

public class StoryManager : MonoBehaviourSingleton<StoryManager>
{
    private readonly Dictionary<string, StoryScene> _sceneDictionary = new Dictionary<string, StoryScene>();

    protected override bool DestroyOnLoad
    {
        get { return true; }
    }

    public float DefaultEndDelay = 0.5f;

    public GameplayTextMode GameplayTextMode = GameplayTextMode.Timer;
    private CharacterStory _riggerStory;
    private CharacterStory _architectStory;

    protected override void InitSingletonInstance()
    {
        SceneData[] scenes = Resources.LoadAll<SceneData>(ResourcePaths.StoryEngineScenePath);
        foreach(var sceneData in scenes)
        {
            StoryScene scene = new StoryScene(sceneData);
            _sceneDictionary.Add(sceneData.Key, scene);
        }
    }

    public void Start()
    {
        _riggerStory = gameObject.AddComponent<CharacterStory>();
        _riggerStory.InitWithKey(CharacterKey.Rigger);
        _architectStory = gameObject.AddComponent<CharacterStory>();
        _architectStory.InitWithKey(CharacterKey.Architect);
    }

    public void Update()
    {
        if(_riggerStory.Syncing && _architectStory.Syncing)
        {
            string targetStitch = "";

            string targetRiggerGather = "we";
            string targetArchitectGather = "we";

            if(string.IsNullOrEmpty(_riggerStory.DesiredStitch) == false)
            {
                if(string.IsNullOrEmpty(_architectStory.DesiredStitch) == false)
                {
                    if(string.Compare(_riggerStory.DesiredStitch, _architectStory.DesiredStitch, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        if(Random.Range(0, 2) == 1)
                        {
                            targetStitch = _riggerStory.DesiredStitch;
                            targetRiggerGather = "i";
                            targetArchitectGather = "they";
                        }
                        else
                        {
                            targetStitch = _architectStory.DesiredStitch;
                            targetRiggerGather = "they";
                            targetArchitectGather = "i";
                        }
                    }
                    else
                    {
                        targetStitch = _riggerStory.DesiredStitch;
                    }
                }
                else
                {
                    targetStitch = _riggerStory.DesiredStitch;
                }
            }
            else
            {
                targetStitch = _architectStory.DesiredStitch;
            }

            if(string.IsNullOrEmpty(targetStitch) == false)
            {
                _riggerStory.JumpToStitchInKnot(_riggerStory.CurrentScene.Data.Key, targetStitch, targetRiggerGather);
                _architectStory.JumpToStitchInKnot(_architectStory.CurrentScene.Data.Key, targetStitch, targetArchitectGather);
            }
            
            _architectStory.Syncing = false;
            _riggerStory.Syncing = false;
        }
        else if(_architectStory.Finished && _riggerStory.Finished)
        {
            SceneHandler.Instance.TransitionToScene("title");
        }
    }

    protected override void DestroySingletonInstance()
    {

    }

    public StoryScene GetScene(string sceneKey)
    {
        StoryScene storyScene;
        _sceneDictionary.TryGetValue(sceneKey, out storyScene);
        return storyScene;
    }

    public StoryScene GetScene(CharacterKey characterKey, int storyIndex)
    {
        foreach(StoryScene storyScene in _sceneDictionary.Values)
        {
            if(storyScene.Data.Character == characterKey)
            {
                if(storyScene.Data.SceneIndex == storyIndex)
                {
                    return storyScene;
                }
            }
        }

        return null;
    }
}