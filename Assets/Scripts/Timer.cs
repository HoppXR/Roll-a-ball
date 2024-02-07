using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    private bool timerActive;
    
    void Update()
    {
        if (timerActive)
        {
            timer.text = $"{Time.realtimeSinceStartup: 0.000}";
        }
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
