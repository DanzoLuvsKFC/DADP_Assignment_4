using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBar;
    public float progressValue = 0f;

    void Update()
    {
        progressBar.value = progressValue;
        if (progressValue > 99)
        {
            SceneManager.LoadScene("EndOfGameCanvas");
        }
    }
    
}
