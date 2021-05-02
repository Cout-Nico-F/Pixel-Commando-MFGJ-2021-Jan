using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class BorderFlasher: MonoBehaviour
{
    Image damage, heal;
    Animator healAnimator, damageAnimator;

    private void Start() {
        damage = transform.Find("Damage").GetComponent<Image>();
        damageAnimator = transform.Find("Damage").GetComponent<Animator>();
        heal = transform.Find("Heal").GetComponent<Image>();
        healAnimator = transform.Find("Heal").Find("HealAnimation").GetComponent<Animator>();
    }

    public void FlashBorder(string playerEvent)
    {
        switch (playerEvent)
        {
            case "damage":
                damageAnimator.Play("Base Layer.FlashBorder", -1, 0f);
                break;
            case "heal":
                //duration = 1f;
                healAnimator.Play("Base Layer.UpHealth", -1, 0f);
                break;
            default:
                break;
        }
    }
}