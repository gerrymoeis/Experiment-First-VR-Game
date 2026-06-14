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

    [Header("Interview Canvas")]
    [SerializeField] private GameObject interviewCanvas01;
    [SerializeField] private GameObject interviewCanvas02;

    [Header("Game Flow")]
    [SerializeField] private GameFlowController gameFlow;

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

    // Tombol Submit
    public void SubmitName()
    {
        SubmitName(nameInputField.text);
    }

    // On End Edit
    public void SubmitName(string enteredName)
    {
        if (playerNameText == null ||
            nameInputPanel == null)
            return;

        string finalName = enteredName.Trim();

        if (string.IsNullOrEmpty(finalName))
            return;

        playerNameText.text = finalName;

        nameInputPanel.SetActive(false);

        assignedRoom = Random.Range(1, 3);

        if (gameFlow != null)
        {
            gameFlow.SetTask(
                $"1. Masuk Ruang {assignedRoom:00}\n" +
                "2. Klik tombol Mulai Interview");
        }

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