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

    float duration = 0.5f;

    private float startTime;
    private float endTime;
    private float t;
    Color flashColor;
    Image damage, heal;


    private void Start() {
        damage = transform.Find("Damage").GetComponent<Image>();
        heal = transform.Find("Heal").GetComponent<Image>();
        img = damage;
    }
    
    // Update is called once per frame
    void Update() {
        var t = ((Time.time - startTime) / (endTime-startTime) );
        img.color = Color.Lerp(visible, transparent, t);
    }

    public void FlashBorder(string playerEvent)
    {
        startTime = Time.time;
        endTime = startTime + duration;
        switch (playerEvent)
        {
            case "damage":
                img = damage;
                damage.color = visible;
                break;
            case "heal":
                img = heal;
                heal.color = visible;
                break;
            default:
                flashColor = transparent;
                break;
        }
    }
}