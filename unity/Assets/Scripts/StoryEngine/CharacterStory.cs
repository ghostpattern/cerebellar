using System;
using System.Collections;
using Ink.Runtime;
using UnityEngine;

public class CharacterStory : MonoBehaviour
{
    public CharacterKey Key;

    private StoryFeed _storyFeed;

    private Story _inkStory;

    private int _nextSceneIndex;
    public string DesiredStitch;
    public StoryScene CurrentScene { get; private set; }

    public void InitWithKey(CharacterKey key)
    {
        Key = key;

        StoryFeed[] _storyFeeds = FindObjectsOfType<StoryFeed>();

        foreach(StoryFeed storyFeed in _storyFeeds)
        {
            if(storyFeed.CharacterKey == key)
            {
                _storyFeed = storyFeed;
                break;
            }
        }

        TextAsset inkStoryJson = Resources.Load<TextAsset>(ResourcePaths.StoryEngineInkJsonPath);
        _inkStory = Story.CreateWithJson(inkStoryJson.text);

        StoryScene debugSceneToPlay = StoryManager.Instance.GetScene(DebugSettings.Instance.LoadIntoScene);

        if(debugSceneToPlay == null && string.IsNullOrEmpty(DebugSettings.Instance.LoadIntoScene) == false)
        {
            int i;
            if(int.TryParse(DebugSettings.Instance.LoadIntoScene, out i))
            {
                debugSceneToPlay = StoryManager.Instance.GetScene(Key, i);
                _nextSceneIndex = i + 1;
            }
        }

        if(debugSceneToPlay != null)
        {
            StartScene(debugSceneToPlay);
        }
        else
        {
            // todo - load the first scene!
            StartScene(StoryManager.Instance.GetScene("intro"));
        }

    }

    private void StartScene(StoryScene scene)
    {
        StartCoroutine(PlaySceneCoroutine(scene));
    }

    public void JumpToStitchInKnot(string sceneKey, string targetStitch, string targetGather = "")
    {
        _storyFeed.ClearOptions();

        if(string.IsNullOrEmpty(targetGather))
        {
            _inkStory.ChoosePathString(sceneKey + "." + targetStitch);
        }
        else
        {
            _inkStory.ChoosePathString(sceneKey + "." + targetStitch + "_" + targetGather);
        }
    }

    private IEnumerator PlaySceneCoroutine(StoryScene scene, float initialDelay = 0.0f)
    {
        CurrentScene = scene;
        // _storyOptionBox.ClearOptions();
        _storyFeed.ClearOptions();

        if(string.IsNullOrEmpty(scene.Data.Name) == false)
        {
            _storyFeed.Clear();
            yield return StartCoroutine(_storyFeed.DisplaySceneName(scene));
        }

        _inkStory.ChoosePathString(scene.Data.Key);

        {
            float timer = 0.0f;
            while(timer < initialDelay)
            {
                timer += Time.deltaTime;

                yield return null;

                if((StoryManager.Instance.GameplayTextMode == GameplayTextMode.ClickToContinue && Input.GetMouseButtonDown(0)) ||
#if MULTI_TOUCH_SKIP
                    (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began) ||
#endif
                    (Application.isEditor && Input.GetMouseButtonDown(1)))
                {
                    break;
                }
            }
        }

        while(_inkStory.canContinue)
        {
            string inkLine = _inkStory.Continue();

            if(!ParseAction(scene, inkLine))
            {
                float delayTime;
                inkLine = ParseDisplayTime(inkLine, out delayTime);

                delayTime = delayTime < 0.0f ? StoryManager.Instance.DefaultEndDelay : delayTime;

                _storyFeed.DisplayLine(inkLine);

                while(_storyFeed.DisplayingLine)
                {
                    yield return null;
                }

                float timer = 0.0f;
                while(timer < delayTime)
                {
                    timer += Time.deltaTime;

                    yield return null;

                    if((StoryManager.Instance.GameplayTextMode == GameplayTextMode.ClickToContinue && Input.GetMouseButtonDown(0)) ||
#if MULTI_TOUCH_SKIP
                        (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began) ||
#endif
                        (Application.isEditor && Input.GetMouseButtonDown(1)))
                    {
                        break;
                    }
                }
            }

            while(Syncing)
            {
                yield return null;
            }

            if(_inkStory.canContinue == false)
            {
                DisplayCurrentInkChoices();

                while(_inkStory.canContinue == false && _inkStory.currentChoices.Count > 0)
                {
                    yield return null;
                }
            }
        }

        PlayNextScene();
    }

    private void PlayNextScene()
    {
        StoryScene storyScene = StoryManager.Instance.GetScene(Key, _nextSceneIndex);
        _nextSceneIndex++;

        while(storyScene == null)
        {
            storyScene = StoryManager.Instance.GetScene(Key, _nextSceneIndex);
            _nextSceneIndex++;
            if(_nextSceneIndex >= 10)
            {
                Finished = true;
                return;
            }
        }

        StartCoroutine(PlaySceneCoroutine(storyScene));
    }

    public bool Finished { get; private set; }

    private void DisplayCurrentInkChoices()
    {
        for(int i = 0; i < _inkStory.currentChoices.Count; i++)
        {
            ChoiceInstance choiceInstance = _inkStory.currentChoices[i];
            int currChoiceIndex = i;
            _storyFeed.DisplayOptionLine(choiceInstance.choiceText, () =>
            {
                _storyFeed.ClearOptions();
                _inkStory.ChooseChoiceIndex(currChoiceIndex);
            });
        }
    }

    private string ParseDisplayTime(string line, out float displayTime)
    {
        displayTime = -1.0f;

        if(line.EndsWith("]\n"))
        {
            string[] valueAndTime = line.Split('[');
            if(valueAndTime.Length > 1)
            {
                line = valueAndTime[0].Trim(' ', '\t');
                if(float.TryParse(valueAndTime[1].Substring(0, valueAndTime[1].IndexOf("s", StringComparison.Ordinal)), out displayTime) == false)
                {
                    displayTime = -1.0f;
                }
            }
        }

        return line;
    }

    private bool ParseAction(StoryScene scene, string value)
    {
        value = value.Trim('.', '\n', ' ');

        string[] colonSplit = value.Split(':');
        
        if(string.Compare(colonSplit[0], "sync", StringComparison.OrdinalIgnoreCase) == 0)
        {
            DesiredStitch = colonSplit.Length > 1 ? colonSplit[1] : "";

            Syncing = true;

            return true;
        }

        return false;
    }

    public bool Syncing { get; set; }
}