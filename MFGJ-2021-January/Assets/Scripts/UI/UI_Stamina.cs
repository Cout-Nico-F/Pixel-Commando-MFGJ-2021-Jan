using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stamina : MonoBehaviour
{
    public Slider staminaSlider;
    PlayerController playerController;

    private void Awake() {
        //Initialize health at full health
        playerController = FindObjectOfType<PlayerController>();
        staminaSlider.gameObject.SetActive(true);
        staminaSlider.maxValue = playerController.MaxStamina;
    }

    public void SetUIStamina(float stamina, float maxStamina)
    {
        //Health is set to (100,100) upon death. This avoids seeing full health in UI when dying.
        if (playerController.gameObject.activeSelf){
            staminaSlider.value = stamina;
            staminaSlider.maxValue = playerController.MaxStamina;
        }else{
            staminaSlider.value = 0;
            staminaSlider.maxValue = maxStamina;
        }
    }
}
