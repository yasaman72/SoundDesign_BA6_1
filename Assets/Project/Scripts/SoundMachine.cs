using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundMachine : MonoBehaviour
{
    [SerializeField] private Gradient backgroundGradient;
    [SerializeField] private Gradient fillGradient;
    [SerializeField] private Image backgroundColor;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Image sliderNode;
    [SerializeField] private TextMeshProUGUI daytimeText;
    [Space] 
    [SerializeField] private SoundData[] soundData;

    private void OnEnable()
    {
        OnSliderValueChanged(0);
    }
    
    public void OnSliderValueChanged(Single value)
    {
        backgroundColor.color = backgroundGradient.Evaluate(value / 100);
        sliderFill.color = fillGradient.Evaluate(value / 100);
        sliderNode.color = fillGradient.Evaluate(value / 100);

        foreach (var data in soundData)
        {
            if (value >= data.startTime && value < data.endTime)
            {
                daytimeText.text = data.dayTimeName;
                break;
            }
        }
    }
}

[Serializable]
struct SoundData
{
    public string dayTimeName;
    public AudioClip clip;
    public float startTime;
    public float endTime;
}