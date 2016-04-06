using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class QuestionnaireButtonResponse
{
    public Button SelectionButton;
    public Image SelectedImage;
}

public class QuestionnaireSelectableResponse : QuestionnaireResponse
{
    public GameObject ResponseParent;
    public GameObject AnswerParent;

    public QuestionnaireButtonResponse[] Responses;

    public void Start()
    {
        for(int i = 0; i < Responses.Length; i++)
        {
            QuestionnaireButtonResponse questionnaireButtonResponse = Responses[i];
            questionnaireButtonResponse.SelectionButton.onClick.AddListener(() => ProcessResponseSelected(questionnaireButtonResponse));
        }

        Reset();
    }

    public override void Reset()
    {
        ResponseParent.SetActive(true);
        AnswerParent.SetActive(false);
    }

    private void ProcessResponseSelected(QuestionnaireButtonResponse selectedResponse)
    {
        ResponseParent.SetActive(false);
        AnswerParent.SetActive(true);

        foreach(QuestionnaireButtonResponse response in Responses)
        {
            response.SelectedImage.gameObject.SetActive(response == selectedResponse);
        }
    }
}
