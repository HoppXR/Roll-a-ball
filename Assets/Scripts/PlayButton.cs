using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        // Freezes time
        Time.timeScale = 0;
    }

    public void BeginGame()
    {
        // Resumes time
        Time.timeScale = 1;
    }
}
