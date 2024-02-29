using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    private string _currentSceneName;
    
    void Start()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void Back()
    {
        SceneManager.LoadScene(_currentSceneName);
    }
}
