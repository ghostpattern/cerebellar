using UnityEngine;
using UnityEngine.UI;

public class KeyboardSelection : MonoBehaviour
{
    public KeyCode InputKey = KeyCode.None;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(InputKey))
        {
            Button button = GetComponent<Button>();
            if (button)
            {
                button.onClick.Invoke();
            }
        }
	}
}
