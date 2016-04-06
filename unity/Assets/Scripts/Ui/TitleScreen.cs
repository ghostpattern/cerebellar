using UnityEngine;

public class TitleScreen : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!Questionnaire.Instance.IsShown)
            {
                Questionnaire.Instance.Show();
            }
            else
            {
                Questionnaire.Instance.Hide();
            }
        }
    }
}
