using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jauge : MonoBehaviour
{
    [SerializeField]
    public int stabilityGauge;
    [SerializeField]
    private int stabilityGaugeMax;
    private Image gauge;

    void Start()
    {
        gauge = GetComponent<Image>();
        stabilityGaugeMax = 24000;
        stabilityGauge = 24000;
    }
    
    public void UpdateHealth()
    {
        stabilityGauge = Mathf.Clamp(stabilityGauge, 0, stabilityGaugeMax);
        float amount = (float)stabilityGauge / stabilityGaugeMax;
        gauge.fillAmount = amount;
    }
}
