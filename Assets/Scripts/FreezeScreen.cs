using UnityEngine;

public class FreezeScreen : MonoBehaviour
{
    private bool isFrozen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isFrozen = !isFrozen;
            Time.timeScale = isFrozen ? 0f : 1f;
        }

    }
}
