using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviourSingleton<SceneHandler>
{
    public AudioSource SceneSource;
    public Image Fader;

    private bool _transitioning;

    protected override bool DestroyOnLoad {
        get { return true; }
    }
    protected override void InitSingletonInstance()
    {

    }

    protected override void DestroySingletonInstance()
    {
    }

    void Start()
    {
        Fader.gameObject.SetActive(true);
        Image uiImage = Fader.GetComponent<Image>();
        uiImage.color = Color.white;
        LTDescr tween = LeanTween.alpha(Fader.rectTransform, 0.0f, 0.667f);
        tween.tweenType = LeanTweenType.easeInOutQuad;
        tween.onComplete = () => Fader.gameObject.SetActive(false);

        LeanTween.value(SceneSource.gameObject, v => SceneSource.volume = v, 0.0f, 1.0f, 5.0f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            SceneManager.LoadScene("title");
        }
    }

    public void TransitionToScene(string sceneName, float fadeTime = 1.5f, float postFadeTime = 0.5f)
    {
        if(_transitioning)
            return;

        LeanTween.cancel(SceneSource.gameObject);
        LeanTween.cancel(Fader.gameObject);

        _transitioning = true;
        Ui.Instance.DisableInteractionOnLayersBelow(UiLayer.Overlay);

        Fader.gameObject.SetActive(true);
        LeanTween.alpha(Fader.rectTransform, 1.0f, fadeTime).tweenType = LeanTweenType.easeInOutQuad;

        LeanTween.value(SceneSource.gameObject, v => SceneSource.volume = v, 1.0f, 0.0f, fadeTime);

        StartCoroutine(TransitionCoroutine(sceneName, fadeTime + postFadeTime));
    }

    private IEnumerator TransitionCoroutine(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(sceneName);
    }
}
