using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum QuestionKey
{
    questionnaire_feeling,
    questionnaire_pain,
    questionnaire_pain_type,
    questionnaire_pain_location,
    questionnaire_pain_amount,
    questionnaire_mental_state,
    questionnaire_emotional_state
}

public class Questionnaire : MonoBehaviourSingleton<Questionnaire>
{
    protected override bool DestroyOnLoad { get { return true; } }
    public bool IsShown { get; set; }

    public Vector3 OnScreenPosition;
    public Question[] RootQuestions;
    public QuestionnaireResponse[] Responses;
    public float TweenTime = 1.0f;
    public LeanTweenType TweenType = LeanTweenType.easeInOutQuad;
    public Button DismissButton;
    private Vector3 _offScreenPosition;
    private int _tweenId = -1;

    protected override void InitSingletonInstance()
    {
        DismissButton.onClick.AddListener(Hide);
        _offScreenPosition = GetComponent<RectTransform>().anchoredPosition3D;
    }

    protected override void DestroySingletonInstance()
    {

    }

    public void InitForScene(StoryScene scene, List<string> enabledQuestions)
    {
        List<QuestionKey> responseKeys = new List<QuestionKey>();
        if(enabledQuestions != null)
        {
            foreach(string enabledQuestion in enabledQuestions)
            {
                try
                {
                    responseKeys.Add((QuestionKey)Enum.Parse(typeof(QuestionKey), enabledQuestion, true));
                }
                catch(Exception)
                {
                    Debug.LogWarning("Questionnaire.InitForScene: " + scene.Data.Key + " attempted to enable " + enabledQuestion + ", which does not exist in QuestionKey enum.");
                }
            }
        }

        foreach(Question question in RootQuestions)
        {
            question.InitForScene(scene, responseKeys, true);
        }

        foreach(QuestionnaireResponse response in Responses)
        {
            response.Reset();
        }
    }

    public void Update()
    {
        if(IsShown && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Hide();
        }
    }

    public void Show()
    {
        if(LeanTween.isTweening(_tweenId))
        {
            LeanTween.cancel(_tweenId);
            _tweenId = -1;
        }

        Ui.Instance.DisableInteractionOnLayersBelow(UiLayer.Overlay);

        LTDescr tween = LeanTween.move(GetComponent<RectTransform>(), OnScreenPosition, TweenTime);
        tween.tweenType = TweenType;
        _tweenId = tween.id;

        IsShown = true;
    }

    public void Hide()
    {
        IsShown = false;
        if(LeanTween.isTweening(_tweenId))
        {
            LeanTween.cancel(_tweenId);
            _tweenId = -1;
        }

        LTDescr tween = LeanTween.move(GetComponent<RectTransform>(), _offScreenPosition, TweenTime);
        tween.onComplete = () =>
        {
            Ui.Instance.ReenableInteractionOnLayersBelow(UiLayer.Overlay);

            ScrollRect scrollRect = GetComponent<ScrollRect>();
            scrollRect.verticalNormalizedPosition = 1;
        };
        tween.tweenType = TweenType;
        _tweenId = tween.id;
    }
}
