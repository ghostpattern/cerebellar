using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneHandler.Instance.TransitionToScene("main");
            GetComponent<AudioSource>().Play();
        });
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            SceneHandler.Instance.TransitionToScene("main");
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            LeanTween.value(audioSource.gameObject, v =>
            {
                audioSource.volume = v;
            }, 1.0f, 0.0f, 1.0f);
        }
    }
}
