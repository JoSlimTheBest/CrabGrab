using UnityEngine;
using UnityEngine.UI;

public class ButtonKeyboardTrigger : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (button != null)
            {
                button.onClick.Invoke();
            }
        }
    }
}