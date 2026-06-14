using UnityEngine;
using TMPro;

public class GameFlowController : MonoBehaviour
{
    [Header("Screen Space UI")]
    [SerializeField] private TMP_Text taskText;

    [Header("Welcome UI")]
    [SerializeField] private GameObject welcomeCanvas;

    [Header("Retry UI")]
    [SerializeField] private GameObject retryCanvas;

    [Header("Receptionist UI")]
    [SerializeField] private GameObject receptionistCanvas;

    private void Start()
    {
        if (retryCanvas != null)
            retryCanvas.SetActive(false);

        if (receptionistCanvas != null)
            receptionistCanvas.SetActive(false);

        SetTask("");
    }

    //--------------------------------------------------
    // TASK
    //--------------------------------------------------

    public void SetTask(string message)
    {
        if (taskText != null)
            taskText.text = message;
    }

    //--------------------------------------------------
    // INTERVIEW
    //--------------------------------------------------

    public void InterviewFailed()
    {
        SetTask("Maaf anda tidak lulus interview.\nSilahkan ulangi lagi proses lamar pekerjaan.");

        if (retryCanvas != null)
            retryCanvas.SetActive(true);

        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);
    }

    public void InterviewPassed()
    {
        SetTask("Silahkan kembali ke resepsionis untuk mulai pekerjaan.");

        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);

        if (receptionistCanvas != null)
            receptionistCanvas.SetActive(true);
    }

    //--------------------------------------------------
    // JOB
    //--------------------------------------------------

    public void StartWorking()
    {
        SetTask("Silahkan anda ke ruangan gudang.\nIkuti garis petunjuknya.");

        if (receptionistCanvas != null)
            receptionistCanvas.SetActive(false);
    }

    public void RetryApplication()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
