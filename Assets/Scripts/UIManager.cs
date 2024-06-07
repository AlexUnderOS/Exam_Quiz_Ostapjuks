using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Text factDisplayText;
    public Button[] answerButtons;
    public Slider timerSlider;

    private Text[] answerTexts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeAnswerButtons(System.Action<int> onAnswerSelected)
    {
        answerTexts = new Text[answerButtons.Length];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
            {
                answerTexts[i] = answerButtons[i].GetComponentInChildren<Text>();
                int index = i;
                answerButtons[i].onClick.AddListener(() => onAnswerSelected(index));
            }
            else
            {
                Debug.LogWarning($"Answer button at index {i} is null.");
            }
        }
    }

    public void DisplayQuestion(Question question)
    {
        factDisplayText.text = question.fact;

        for (int i = 0; i < question.answers.Length; i++)
        {
            answerTexts[i].text = question.answers[i].answer;
            answerButtons[i].gameObject.SetActive(true);
        }

        for (int i = question.answers.Length; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(false);
        }
    }

    public void ResetButtonColors(Color deselectColor)
    {
        foreach (Button button in answerButtons)
        {
            button.GetComponent<Image>().color = deselectColor;
        }
    }

    public void SetButtonColor(int index, Color color)
    {
        answerButtons[index].GetComponent<Image>().color = color;
    }

    public void UpdateTimerSlider(float value)
    {
        timerSlider.value = value;
    }
}
