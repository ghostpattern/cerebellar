using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PerCharacterWriter : MonoBehaviour
{
    private string _properText;
    private Text _textComponent;

    public float PerCharacterSpeed = 1.0f;
    public float PerCharacterDelay = 0.04f;
    public bool Animating { get; private set; }

    void Awake()
    {
        Animating = true;
    }

    // Use this for initialization
	void Start ()
    {
        _textComponent = GetComponent<Text>();
	    _properText = _textComponent.text;

	    StartCoroutine(AnimateTextCoroutine());
	}

    private IEnumerator AnimateTextCoroutine()
    {
        Animating = true;
        float timer = 0.0f;
        while(timer < PerCharacterDelay * _properText.Length)
        {
            _textComponent.text = CalculateText((int)(timer / PerCharacterDelay));

            timer += Time.deltaTime * PerCharacterSpeed;

            if(Application.isEditor && Input.GetMouseButtonDown(1))
            {
                break;
            }

            yield return null;
        }

        _textComponent.text = _properText;
        Animating = false;
    }

    private string CalculateText(int currIndex)
    {
        StringBuilder sb = new StringBuilder(_properText.Length);
        sb.Append(_properText.Substring(0, currIndex));
        for(int i = currIndex; i < _properText.Length; i++)
        {
            sb.Append(' ');
        }
        return sb.ToString();
    }
}
