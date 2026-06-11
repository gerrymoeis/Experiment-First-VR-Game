using UnityEngine;
using TMPro;

public class InterviewQuiz : MonoBehaviour
{
    [Header("Question UI")]
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text questionText;

    [Header("Answer UI")]
    [SerializeField] private TMP_Text answerAText;
    [SerializeField] private TMP_Text answerBText;
    [SerializeField] private TMP_Text answerCText;

    [Header("Canvas")]
    [SerializeField] private GameObject interviewCanvas;
    [SerializeField] private GameObject resultCanvas;

    [Header("Result UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text resultText;

    private int currentQuestion = 0;
    private int score = 0;

    private const int TOTAL_QUESTIONS = 3;

    private void Start()
    {
        if (resultCanvas != null)
        {
            resultCanvas.SetActive(false);
        }

        LoadQuestion();
    }

    private void LoadQuestion()
    {
        progressText.text =
            $"{currentQuestion + 1}  /  {TOTAL_QUESTIONS}";

        switch (currentQuestion)
        {
            case 0:

                speakerText.text = "Interviewer";

                questionText.text =
                    "Apa data struktur yang cocok\n" +
                    "untuk mengelola data pencarian\n" +
                    "karyawan dengan instan?";

                answerAText.text = "Queue";
                answerBText.text = "Linked List";
                answerCText.text = "Hash Map";

                break;

            case 1:

                speakerText.text = "Interviewer";

                questionText.text =
                    "Bahasa pemrograman yang umum\n" +
                    "digunakan untuk backend web?";

                answerAText.text = "Go";
                answerBText.text = "HTML";
                answerCText.text = "PNG";

                break;

            case 2:

                speakerText.text = "Interviewer";

                questionText.text =
                    "SQL digunakan untuk apa?";

                answerAText.text = "Menggambar UI";
                answerBText.text = "Mengelola Database";
                answerCText.text = "Editing Video";

                break;
        }
    }

    public void AnswerA()
    {
        CheckAnswer(0);
    }

    public void AnswerB()
    {
        CheckAnswer(1);
    }

    public void AnswerC()
    {
        CheckAnswer(2);
    }

    private void CheckAnswer(int selectedAnswer)
    {
        int correctAnswer = GetCorrectAnswer();

        if (selectedAnswer == correctAnswer)
        {
            score++;
        }

        NextQuestion();
    }

    private int GetCorrectAnswer()
    {
        switch (currentQuestion)
        {
            case 0:
                return 2; // C = Hash Map

            case 1:
                return 0; // A = Go

            case 2:
                return 1; // B = Database
        }

        return -1;
    }

    private void NextQuestion()
    {
        currentQuestion++;

        if (currentQuestion >= TOTAL_QUESTIONS)
        {
            ShowResult();
            return;
        }

        LoadQuestion();
    }

    private void ShowResult()
    {
        if (interviewCanvas != null)
        {
            interviewCanvas.SetActive(false);
        }

        if (resultCanvas != null)
        {
            resultCanvas.SetActive(true);
        }

        scoreText.text =
            $"Nilai: {score} / {TOTAL_QUESTIONS}";

        float percentage =
            (float)score / TOTAL_QUESTIONS;

        bool passed =
            percentage >= 0.5f;

        if (passed)
        {
            resultText.text = "LULUS INTERVIEW";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "TIDAK LULUS INTERVIEW";
            resultText.color = Color.red;
        }
    }
}
