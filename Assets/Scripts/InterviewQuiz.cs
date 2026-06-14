using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InterviewQuiz : MonoBehaviour
{
    [Header("Question Database")]
    [SerializeField] private QuestionData[] questions;

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
    [SerializeField] private GameObject welcomeCanvas;

    [Header("Result UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text resultText;

    [Header("Game Flow")]
    [SerializeField] private GameFlowController gameFlow;

    private List<QuestionData> randomizedQuestions;

    private int currentQuestion = 0;
    private int score = 0;

    private void Start()
    {
        if (resultCanvas != null)
        {
            resultCanvas.SetActive(false);
        }

        randomizedQuestions = new List<QuestionData>(questions);

        ShuffleQuestions();

        LoadQuestion();
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < randomizedQuestions.Count; i++)
        {
            int randomIndex =
                Random.Range(i, randomizedQuestions.Count);

            QuestionData temp =
                randomizedQuestions[i];

            randomizedQuestions[i] =
                randomizedQuestions[randomIndex];

            randomizedQuestions[randomIndex] =
                temp;
        }
    }

    private void LoadQuestion()
    {
        QuestionData question =
            randomizedQuestions[currentQuestion];

        progressText.text =
            $"Soal {currentQuestion + 1} / {randomizedQuestions.Count}";

        speakerText.text =
            question.speaker;

        questionText.text =
            question.question;

        answerAText.text =
            question.answerA;

        answerBText.text =
            question.answerB;

        answerCText.text =
            question.answerC;
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
        QuestionData question =
            randomizedQuestions[currentQuestion];

        if (selectedAnswer == question.correctAnswer)
        {
            score++;
        }

        NextQuestion();
    }

    private void NextQuestion()
    {
        currentQuestion++;

        if (currentQuestion >= randomizedQuestions.Count)
        {
            ShowResult();
            return;
        }

        LoadQuestion();
    }

    private void ShowResult()
    {
        interviewCanvas.SetActive(false);

        resultCanvas.SetActive(true);

        scoreText.text =
            $"Nilai: {score} / {randomizedQuestions.Count}";

        float percentage =
            (float)score / randomizedQuestions.Count;

        bool passed =
            percentage >= 0.5f;

        if (passed)
        {
            resultText.text = "LULUS";
            resultText.color = Color.green;

            if (gameFlow != null)
            {
                gameFlow.InterviewPassed();
            }
        }
        else
        {
            resultText.text = "TIDAK LULUS";
            resultText.color = Color.red;

            if (gameFlow != null)
            {
                gameFlow.InterviewFailed();
            }
        }
    }
}
