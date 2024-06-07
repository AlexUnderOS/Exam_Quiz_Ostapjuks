using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyComponents : MonoBehaviour
{
    public static DontDestroyComponents Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) // Check instance existence
        {
            // If no instance exists, assign the current instance to Instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) // If an instance already exists and it's not the current one
        {
            Destroy(gameObject);
        }
    }
}
