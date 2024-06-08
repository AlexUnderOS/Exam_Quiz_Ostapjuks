[System.Serializable]
public class Question
{
    public string fact;
    public Answer[] answers;

    public string GetFact()
    {
        return fact;
    }
}
