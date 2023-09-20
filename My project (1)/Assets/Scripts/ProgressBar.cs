using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider progressBar;
    public float progressValue = 0f;

    void Update()
    {
        progressBar.value = progressValue;
    }
}
