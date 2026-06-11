using UnityEngine;
using TMPro;

public class JobApplicationUI : MonoBehaviour
{
    [Header("Panel Input Nama")]
    [SerializeField] private GameObject nameInputPanel;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField nameInputField;

    [Header("UI Screen Space")]
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text taskText;

    [Header("Interview Canvas")]
    [SerializeField] private GameObject interviewCanvas01;
    [SerializeField] private GameObject interviewCanvas02;

    private int assignedRoom;

    private void Start()
    {
        if (nameInputPanel != null)
        {
            nameInputPanel.SetActive(false);
        }

        if (interviewCanvas01 != null)
        {
            interviewCanvas01.SetActive(false);
        }

        if (interviewCanvas02 != null)
        {
            interviewCanvas02.SetActive(false);
        }

        if (taskText != null)
        {
            taskText.text = "";
        }
    }

    public void OpenNameInputPanel()
    {
        if (nameInputPanel == null || nameInputField == null)
            return;

        nameInputPanel.SetActive(true);

        nameInputField.text = "";
        nameInputField.ActivateInputField();
        nameInputField.Select();
    }

    // Untuk tombol Submit
    public void SubmitName()
    {
        SubmitName(nameInputField.text);
    }

    // Untuk On End Edit(String)
    public void SubmitName(string enteredName)
    {
        if (playerNameText == null ||
            nameInputPanel == null)
            return;

        string finalName = enteredName.Trim();

        if (string.IsNullOrEmpty(finalName))
            return;

        // Simpan nama ke UI
        playerNameText.text = finalName;

        // Tutup panel input
        nameInputPanel.SetActive(false);

        // Pilih ruangan secara random
        assignedRoom = Random.Range(1, 3);

        // Tampilkan tugas
        if (taskText != null)
        {
            taskText.text =
                $"1. Masuk Ruang {assignedRoom:00}\n" +
                "2. Klik tombol Mulai Interview";
        }

        // Aktifkan canvas interview yang sesuai
        if (assignedRoom == 1)
        {
            interviewCanvas01.SetActive(true);
            interviewCanvas02.SetActive(false);
        }
        else
        {
            interviewCanvas01.SetActive(false);
            interviewCanvas02.SetActive(true);
        }
    }
}
