using UnityEngine;

public class GagalInterviewController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuPanel;

    private void Update()
    {

    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
    }
}
