using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public static SortingManager Instance;

    [Header("Warehouse UI")]
    [SerializeField] private GameObject warehouseIntroCanvas;

    [Header("Gameplay")]
    [SerializeField] private int totalSockets = 4;

    private int filledSockets = 0;
    private bool gameStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartSorting()
    {
        if (gameStarted)
            return;

        gameStarted = true;

        filledSockets = 0;

        if (warehouseIntroCanvas != null)
            warehouseIntroCanvas.SetActive(false);

        GameFlowController.Instance.SetTask(
            "Sortir seluruh barang ke rak yang tersedia.");

        Debug.Log("Gameplay Sortir Dimulai");
    }

    public void SocketFilled()
    {
        filledSockets++;

        Debug.Log($"Socket Terisi : {filledSockets}/{totalSockets}");

        if (filledSockets >= totalSockets)
        {
            SortingCompleted();
        }
    }

    private void SortingCompleted()
    {
        Debug.Log("SORTING SELESAI");

        GameFlowController.Instance.SetTask(
            "Selamat!\nSemua barang berhasil disortir.");
    }
}
