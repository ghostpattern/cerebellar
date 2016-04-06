using UnityEngine;
using UnityEngine.UI;

public class QuestionnaireTrigger : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    GetComponent<Button>().onClick.AddListener(() =>
	    {
	        Questionnaire.Instance.Show();
	    });
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
