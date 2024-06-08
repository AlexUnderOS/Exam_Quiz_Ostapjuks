using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizResults : MonoBehaviour
{
    public GameObject resultsObject;
    public GridLayoutGroup starContainer;
    public QuestionManager qm;
    public Text scoreText;
    public AudioSource resultSource;
    public AudioClip[] resultClips;
    public GameObject confettiToShow;
    public Text passedTestsResultText;

    public Sprite starSprite;


    public void ShowResultsPanel()
    {
        GameObject firstChild = resultsObject.transform.GetChild(0).gameObject;
        firstChild.SetActive(true); // Results Panel!
        UpdateResults();
    }

    private void UpdateResults()
    {
        UpdateText();
        UpdateStarCount();
        resultSource.Play();
    }

    private void UpdateStarCount()
    {
        int score = qm.GetScore();
        int totalQuestions = qm.GetScoreLength();
        float percentage = (float)score / totalQuestions * 100f;

        int starCount;

        if (percentage >= 75f) // 75%
        {
            resultSource.clip = resultClips[0];
            confettiToShow.SetActive(true);
            starCount = 3;
        }
        else if (percentage >= 50f) // 50%
        {
            resultSource.clip = resultClips[0];
            confettiToShow.SetActive(true);
            starCount = 2;
        }
        else if (percentage >= 20f) // 20%
        {
            resultSource.clip = resultClips[1];
            starCount = 1;
        } 
        else
        {
            resultSource.clip = resultClips[1];
            starCount = 0;
        }


        foreach (Transform child in starContainer.transform)
        {
            Destroy(child.gameObject); // reset stars
        }

        if (starCount > 0)
        {
            for (int i = 0; i < starCount; i++)
            {
                GameObject starObject = new GameObject("Star");
                Image starImage = starObject.AddComponent<Image>();
                starImage.sprite = starSprite;
                starObject.transform.SetParent(starContainer.transform); // Set star parent
            }
        }
    }

    private void UpdateText()
    {
        scoreText.text = qm.GetScore() + " / " + qm.GetScoreLength() + "!";
        passedTestsResultText.text = qm.GetResultsLog();
    }
}
