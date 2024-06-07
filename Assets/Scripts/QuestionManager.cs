using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public Question[] questions;
    public Color selectColor;
    public Color deselectColor;
    public QuizResults quizResults;

    private List<int> selectedAnswers;
    private int currentQuestionIndex = 0;
    private int score = 0;
    private Question currentQuestion;

    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private Timer timer;

    private void Start()
    {
        selectedAnswers = new List<int>();

        uiManager.InitializeAnswerButtons(OnAnswerSelected);
        SetCurrentQuestion();
        UpdateScoreDisplay();
    }

    private void SetCurrentQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("No more questions available.");
            quizResults.ShowResultsPanel(); 
            return;
        }

        currentQuestion = questions[currentQuestionIndex];
        uiManager.DisplayQuestion(currentQuestion);

        selectedAnswers.Clear();
        uiManager.ResetButtonColors(deselectColor);
        currentQuestionIndex++;

        timer.StartTimer(() =>
        {
            CheckAnswers();
            SetCurrentQuestion();
        });
    }

    private void OnAnswerSelected(int index)
    {
        if (selectedAnswers.Contains(index))
        {
            selectedAnswers.Remove(index);
            uiManager.SetButtonColor(index, deselectColor);
        }
        else
        {
            selectedAnswers.Add(index);
            uiManager.SetButtonColor(index, selectColor);
        }
    }

    private void CheckAnswers()
    {
        List<int> correctAnswers = currentQuestion.answers
            .Select((answer, index) => new { answer, index })
            .Where(x => x.answer.isCorrect)
            .Select(x => x.index)
            .ToList();

        if (correctAnswers.Count == selectedAnswers.Count && correctAnswers.All(selectedAnswers.Contains))
        {
            Debug.Log("Correct Answers!");
            score++;
            UpdateScoreDisplay();
        }
        else
        {
            Debug.Log("Incorrect Answers, try again.");
        }

        foreach (int index in selectedAnswers)
        {
            uiManager.SetButtonColor(index, deselectColor);
        }

        selectedAnswers.Clear();
    }

    private void UpdateScoreDisplay()
    {
        Debug.Log("Score: " + score);
    }

    public int GetScore()
    {
        return score;
    }

    public int GetScoreLength()
    {
        return questions.Length;
    }
}
