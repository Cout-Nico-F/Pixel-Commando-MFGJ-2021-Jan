using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color lowColor;
    public Color highColor;
    //A health bar with 10 health looks very big, even though you are 1 shot
    //from dying. This "slider correction" makes the health look lower so the 
    //player knows they're about to die.
    private float healthSliderCorrection = 9.5f;

    private void Awake() {
        //Initialize health at full health
        slider.gameObject.SetActive(true);
        slider.maxValue = 100;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowColor, highColor, slider.normalizedValue);
    }

    public void SetUIHealth(float health, float maxHealth)
    {
        slider.value = health - healthSliderCorrection;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowColor, highColor, slider.normalizedValue);
    }
}
