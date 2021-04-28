using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color lowColor;
    public Color highColor;
    public UI_HealthBar uiHealthBar;
    
    private void Awake() {
        uiHealthBar = FindObjectOfType<UI_HealthBar>().GetComponent<UI_HealthBar>();
    }

    //A health bar with 10 health looks very big, even though you are 1 shot
    //from dying. This "slider correction" makes the health look lower so the 
    //player knows they're about to die.
    private int healthSliderCorrection = 5;

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = health - healthSliderCorrection;
        slider.maxValue = maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(lowColor, highColor, slider.normalizedValue);

        uiHealthBar.SetUIHealth(health, maxHealth);
    }

}
