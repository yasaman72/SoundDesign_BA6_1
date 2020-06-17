using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundMachine : MonoBehaviour
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private TextMeshProUGUI daytimeText;
    [Space] 
    [SerializeField] private SoundData[] soundData;

    private void OnEnable()
    {
        backgroundColor.color = gradient.Evaluate(0);
    }

    public void OnSliderValueChanged(Single value)
    {
        backgroundColor.color = gradient.Evaluate(value / 100);
    }
}

[System.Serializable]
struct SoundData
{
    public string dayTimeName;
    public AudioClip clip;
    public float startTime;
    public float endTime;
}