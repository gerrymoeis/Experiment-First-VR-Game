using UnityEngine;
using TMPro;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController Instance;

    [Header("Screen Space UI")]
    [SerializeField] private TMP_Text taskText;

    [Header("Welcome UI")]
    [SerializeField] private GameObject welcomeCanvas;

    [Header("Receptionist UI")]
    [SerializeField] private GameObject receptionistCanvas;

    [Header("Retry UI")]
    [SerializeField] private GameObject retryCanvas;

    [Header("Gameplay")]
    [SerializeField] private GameObject navigationLine;

    [SerializeField] private GameObject warehouseIntroCanvas;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (navigationLine != null)
            navigationLine.SetActive(false);

        if (warehouseIntroCanvas != null)
            warehouseIntroCanvas.SetActive(false);

        if (receptionistCanvas != null)
            receptionistCanvas.SetActive(false);

        if (retryCanvas != null)
            retryCanvas.SetActive(false);
    }

    //-------------------------------------------------

    public void InterviewPassed()
    {
        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);

        if (receptionistCanvas != null)
            receptionistCanvas.SetActive(true);

        SetTask(
            "Silahkan kembali ke resepsionis\nuntuk memulai pekerjaan.");
    }

    //-------------------------------------------------

    public void InterviewFailed()
    {
        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);

        if (retryCanvas != null)
            retryCanvas.SetActive(true);

        SetTask(
            "Maaf anda tidak lulus interview.\nSilahkan ulangi proses melamar pekerjaan.");
    }

    //-------------------------------------------------

    public void StartWorking()
    {
        if (receptionistCanvas != null)
            receptionistCanvas.SetActive(false);

        if (navigationLine != null)
            navigationLine.SetActive(true);

        SetTask(
            "Silahkan menuju Gudang.\nIkuti garis petunjuk.");
    }

    //-------------------------------------------------

    public void ArriveWarehouse()
    {
        Debug.Log("ArriveWarehouse dipanggil");

        if (navigationLine != null)
            navigationLine.SetActive(false);

        if (warehouseIntroCanvas != null)
            warehouseIntroCanvas.SetActive(true);
    }

    public void SortingFinished()
    {
        SetTask(
            "Pekerjaan hari ini selesai.\nKembali ke resepsionis untuk izin pulang.");
    }

    //-------------------------------------------------

    public void SetTask(string task)
    {
        if (taskText != null)
            taskText.text = task;
    }
}
