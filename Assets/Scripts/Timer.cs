using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timer;
    private bool _timerActive;
    public float time;
    
    void Update()
    {
        time += Time.deltaTime;
        
        if (_timerActive)
        {
            timer.text = $"{time: 0.000}";
        }
    }

    public void StartTimer()
    {
        _timerActive = true;
    }

    public void StopTimer()
    {
        _timerActive = false;
    }
}
