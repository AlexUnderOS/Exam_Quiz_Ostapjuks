using UnityEngine;

public class Timer : MonoBehaviour
{
    public float duration = 10f;
    private float timeRemaining;
    private System.Action onTimeUp;
    private bool isRunning = false;

    public void StartTimer(System.Action onTimeUpCallback)
    {
        timeRemaining = duration;
        onTimeUp = onTimeUpCallback;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void Update()
    {
        if (!isRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float normalizedTime = timeRemaining / duration;
            UIManager.Instance.UpdateTimerSlider(normalizedTime);

            if (timeRemaining <= 0)
            {
                isRunning = false;
                onTimeUp?.Invoke();
            }
        }
    }
}
