using UnityEngine;

[System.Serializable]
public class Question
{
    [TextArea]
    public string fact;

    public Answer[] answers;

    public string GetFact()
    {
        return fact;
    }
}
