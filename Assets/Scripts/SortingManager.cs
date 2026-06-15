using UnityEngine;
using TMPro;

public class SortingManager : MonoBehaviour
{
    public static SortingManager Instance;

    [Header("Warehouse UI")]
    [SerializeField] private GameObject warehouseIntroCanvas;

    [Header("Gameplay UI")]
    [SerializeField] private GameObject sortingProgressUI;
    [SerializeField] private TMP_Text sortingProgressText;

    [Header("Timer UI")]
    [SerializeField] private GameObject timerCanvas;
    [SerializeField] private TMP_Text timerText;

    [Header("Result UI")]
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private TMP_Text resultTitleText;
    [SerializeField] private TMP_Text resultDescriptionText;
    [SerializeField] private TMP_Text resultButtonText;

    [Header("Gameplay")]
    [SerializeField] private int totalSockets = 4;
    [SerializeField] private SortableItem[] sortableItems;
    [SerializeField] private float sortingTime = 60f;

    private int filledSockets = 0;

    private bool gameStarted = false;
    private bool gameFinished = false;
    private bool lastResultSuccess = false;

    private float currentTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (sortingProgressUI != null)
            sortingProgressUI.SetActive(false);

        if (timerCanvas != null)
            timerCanvas.SetActive(false);

        if (resultCanvas != null)
            resultCanvas.SetActive(false);

        currentTime = sortingTime;
        UpdateTimerUI();
    }

    private void Update()
    {
        if (!gameStarted)
            return;

        if (gameFinished)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            UpdateTimerUI();
            TimeUp();
            return;
        }

        UpdateTimerUI();
    }

    public void StartSorting()
    {
        if (gameStarted)
            return;

        gameStarted = true;
        gameFinished = false;
        filledSockets = 0;
        currentTime = sortingTime;

        if (warehouseIntroCanvas != null)
            warehouseIntroCanvas.SetActive(false);

        if (sortingProgressUI != null)
            sortingProgressUI.SetActive(true);

        if (timerCanvas != null)
            timerCanvas.SetActive(true);

        if (resultCanvas != null)
            resultCanvas.SetActive(false);

        UpdateSortingUI();
        UpdateTimerUI();

        GameFlowController.Instance.SetTask(
            "Sortir seluruh barang ke rak yang tersedia.");

        Debug.Log("Gameplay Sortir Dimulai");
    }

    public void SocketFilled()
    {
        if (gameFinished)
            return;

        filledSockets++;
        UpdateSortingUI();

        Debug.Log($"SORTING TERISI {filledSockets}");

        if (filledSockets >= totalSockets)
        {
            SortingCompleted();
        }
    }

    public void SocketEmptied()
    {
        if (gameFinished)
            return;

        filledSockets--;

        if (filledSockets < 0)
            filledSockets = 0;

        UpdateSortingUI();

        Debug.Log($"SORTING BERKURANG {filledSockets}");
    }

    private void UpdateSortingUI()
    {
        if (sortingProgressText != null)
        {
            sortingProgressText.text =
                $"Sortir Barang\n{filledSockets} / {totalSockets}";
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void SortingCompleted()
    {
        gameFinished = true;
        lastResultSuccess = true;

        if (sortingProgressUI != null)
            sortingProgressUI.SetActive(false);

        if (timerCanvas != null)
            timerCanvas.SetActive(false);

        ShowResult(
            "Pekerjaan Selesai",
            "Semua barang berhasil disortir.\nKerja bagus.",
            "Lanjut"
        );

        Debug.Log("SORTING SELESAI");
    }

    private void TimeUp()
    {
        gameFinished = true;
        lastResultSuccess = false;

        if (sortingProgressUI != null)
            sortingProgressUI.SetActive(false);

        if (timerCanvas != null)
            timerCanvas.SetActive(false);

        ShowResult(
            "Pekerjaan Gagal",
            "Masih ada barang yang belum disortir.\nSilahkan ulangi pekerjaan.",
            "Ulangi"
        );

        Debug.Log("WAKTU HABIS");
    }

    private void ShowResult(string title, string desc, string buttonText)
    {
        if (resultCanvas != null)
            resultCanvas.SetActive(true);

        if (resultTitleText != null)
            resultTitleText.text = title;

        if (resultDescriptionText != null)
            resultDescriptionText.text = desc;

        if (resultButtonText != null)
            resultButtonText.text = buttonText;
    }

    private void ResetSorting()
    {
        gameStarted = false;
        gameFinished = false;

        filledSockets = 0;
        currentTime = sortingTime;

        UpdateSortingUI();
        UpdateTimerUI();

        foreach (SortableItem item in sortableItems)
        {
            if (item != null)
                item.ResetItem();
        }

        if (sortingProgressUI != null)
            sortingProgressUI.SetActive(false);

        if (timerCanvas != null)
            timerCanvas.SetActive(false);

        if (resultCanvas != null)
            resultCanvas.SetActive(false);

        if (warehouseIntroCanvas != null)
            warehouseIntroCanvas.SetActive(true);

        GameFlowController.Instance.SetTask(
            "Sortir seluruh barang ke rak yang tersedia.");
    }

    public void ResultButtonPressed()
    {
        Debug.Log("RESULT BUTTON DITEKAN");
        Debug.Log("lastResultSuccess = " + lastResultSuccess);

        if (lastResultSuccess)
        {
            Debug.Log("MASUK BRANCH SUCCESS");

            if (resultCanvas != null)
                resultCanvas.SetActive(false);

            GameFlowController.Instance.SortingFinished();
        }
        else
        {
            Debug.Log("MASUK BRANCH FAIL");
            RetrySorting();
        }
    }

    public void RetrySorting()
    {
        ResetSorting();
    }
}
