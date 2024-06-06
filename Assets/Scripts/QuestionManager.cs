using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Question[] questions;
    public Button answerButton0;
    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;

    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text factDisplayText;

    private void Start()
    {
        unansweredQuestions = new List<Question>();
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList();
        }

        SetCurrentQuestion();

    }

    private void SetCurrentQuestion()
    {
        int randQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randQuestionIndex];

        factDisplayText.text = currentQuestion.fact;

        unansweredQuestions.RemoveAt(randQuestionIndex);
    }
}
