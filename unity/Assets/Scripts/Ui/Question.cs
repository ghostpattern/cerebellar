using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public QuestionKey Key;

    public Button QuestionButton;
    public Text AskedQuestionText;
    public Question[] SubQuestions;
    public bool HasResponseInScene { get; private set; }

    // Use this for initialization
    public void InitForScene(StoryScene scene, List<QuestionKey> responseKeys, bool questionInitiallyActive)
    {
        if(QuestionButton != null)
        {
            HasResponseInScene = responseKeys.Contains(Key);

            if(HasResponseInScene)
            {
                QuestionButton.onClick.RemoveAllListeners();
                QuestionButton.onClick.AddListener(
                    () =>
                    {
                        Questionnaire.Instance.Hide();
                        // StoryManager.Instance.JumpToStitchInKnot(scene.Data.Key, Key.ToString());

                        SetQuestionActive(false);

                        foreach(Question subQuestion in SubQuestions)
                        {
                            if(subQuestion.HasResponseInScene)
                            {
                                subQuestion.SetQuestionActive(true);
                            }
                        }
                    });
            }

            foreach(Question subQuestion in SubQuestions)
            {
                subQuestion.InitForScene(scene, responseKeys, HasResponseInScene == false);
            }

            SetQuestionActive(HasResponseInScene && questionInitiallyActive);
        }
    }

    private void SetQuestionActive(bool active)
    {
        QuestionButton.gameObject.SetActive(active);

        if(AskedQuestionText != null)
        {
            AskedQuestionText.gameObject.SetActive(!active);
        }
    }
}
