using UnityEngine;
using UnityEngine.UI;

public class KeyToButton : MonoBehaviour
{
    public Button targetButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetButton.onClick.Invoke();
        }
    }
}
