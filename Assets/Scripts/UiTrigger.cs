using UnityEngine;

public class UIToggleTrigger : MonoBehaviour
{
    public GameObject uiPanel; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }
}
