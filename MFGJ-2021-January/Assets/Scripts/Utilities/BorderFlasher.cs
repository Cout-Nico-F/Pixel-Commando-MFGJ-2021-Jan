using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class BorderFlasher: MonoBehaviour
{
    Image img;
    // Start is called before the first frame update
    Color transparent = new Color (1f, 0f, 0f, 0f);
    Color visible = new Color (1f, 1f, 1f, 0.4f);

    float duration;

    private float startTime;
    private float endTime;
    private float t;
    Color flashColor;
    Image damage, heal;
    Animator healAnimator;

    private void Start() {
        damage = transform.Find("Damage").GetComponent<Image>();
        heal = transform.Find("Heal").GetComponent<Image>();
        healAnimator = transform.Find("Heal").Find("HealAnimation").GetComponent<Animator>();
        img = damage;
    }
    
    // Update is called once per frame
    void Update() {
        var t = ((Time.time - startTime) / (endTime-startTime) );
        img.color = Color.Lerp(visible, transparent, t);
    }

    public void FlashBorder(string playerEvent)
    {
        switch (playerEvent)
        {
            case "damage":
                duration = 0.5f;
                img = damage;
                img.color = visible;
                break;
            case "heal":
                duration = 1f;
                img = heal;
                img.color = visible;
                healAnimator.Play("Base Layer.UpHealth", -1, 0f);
                break;
            default:
                img.color = transparent;
                break;
        }
        startTime = Time.time;
        endTime = startTime + duration;
    }
}