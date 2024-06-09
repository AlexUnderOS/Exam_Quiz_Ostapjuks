using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class SentenceType : MonoBehaviour
{
    private Text displayText;
    public float delay = 0.1f;

    private void Start()
    {
        displayText = GetComponent<Text>();
        StartCoroutine(TypeSentence());
    }

    private IEnumerator TypeSentence()
    {
        string sentence = displayText.text;
        displayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}
