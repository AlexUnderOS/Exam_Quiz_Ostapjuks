using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public Question[] questions;
    public Button[] answerButtons;
    public Slider timerSlider;

    public Color selectColor;
    public Color deselectColor;

    private Text[] answerTexts;
    private List<int> selectedAnswers;
    private int currentQuestionIndex = 0;
    private int score = 0;
    public float timerDuration = 10f;
    private float timeRemaining;

    private Question currentQuestion;

    [SerializeField]
    private Text factDisplayText;

    private void Start()
    {
        selectedAnswers = new List<int>();

        InitializeAnswerButtons();
        SetCurrentQuestion();
        UpdateScoreDisplay();
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerSlider.value = timeRemaining / timerDuration;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                CheckAnswers();
                SetCurrentQuestion();
            }
        }
    }

    private void InitializeAnswerButtons()
    {
        if (answerButtons == null || answerButtons.Length == 0)
        {
            Debug.LogError("Answer buttons array is not set or empty.");
            return;
        }

        answerTexts = new Text[answerButtons.Length];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
            {
                answerTexts[i] = answerButtons[i].GetComponentInChildren<Text>();
                int index = i;
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else
            {
                Debug.LogWarning($"Answer button at index {i} is null.");
            }
        }
    }

    private void SetCurrentQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("No more questions available.");
            return;
        }

        currentQuestion = questions[currentQuestionIndex];

        factDisplayText.text = currentQuestion.fact;

        for (int i = 0; i < currentQuestion.answers.Length; i++)
        {
            answerTexts[i].text = currentQuestion.answers[i].answer;
            answerButtons[i].gameObject.SetActive(true);
        }

        // Hide unused buttons
        for (int i = currentQuestion.answers.Length; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(false);
        }

        selectedAnswers.Clear();
        ResetButtonColors();
        currentQuestionIndex++;
        timeRemaining = timerDuration;
        timerSlider.value = 1;
    }

    private void OnAnswerSelected(int index)
    {
        if (selectedAnswers.Contains(index))
        {
            selectedAnswers.Remove(index);
            answerButtons[index].GetComponent<Image>().color = deselectColor; // Reset color
            Debug.Log($"Deselected button {index}");
        }
        else
        {
            selectedAnswers.Add(index);
            answerButtons[index].GetComponent<Image>().color = selectColor; // Mark as selected
            Debug.Log($"Selected button {index}");
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
            answerButtons[index].GetComponent<Image>().color = deselectColor;
        }

        selectedAnswers.Clear();
    }

    private void UpdateScoreDisplay()
    {
        Debug.Log("Score: " + score);
    }

    private void ResetButtonColors()
    {
        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().color = deselectColor;
        }
    }
}
