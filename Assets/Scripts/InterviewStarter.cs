using UnityEngine;

public class InterviewStarter : MonoBehaviour
{
    [SerializeField] private GameObject readyCanvas;
    [SerializeField] private GameObject interviewCanvas;

    private void Start()
    {
        if (interviewCanvas != null)
        {
            interviewCanvas.SetActive(false);
        }
    }

    public void StartInterview()
    {
        if (readyCanvas != null)
        {
            readyCanvas.SetActive(false);
        }

        if (interviewCanvas != null)
        {
            interviewCanvas.SetActive(true);
        }
    }
}
