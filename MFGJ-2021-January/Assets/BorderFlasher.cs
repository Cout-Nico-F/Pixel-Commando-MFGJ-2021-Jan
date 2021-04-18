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
    Color redFlash = new Color (1f, 0f, 0f, 0.4f);

    float duration = 0.5f;

    private float startTime;
    private float endTime;
    private float t;
    Color flashColor;

    void Start ()
    {
        img = gameObject.GetComponent<Image>();
    }
    // Update is called once per frame

    void Update() {
        var t = ((Time.time - startTime) / (endTime-startTime) );
        
        img.color = Color.Lerp(flashColor, transparent, t);
    }


    public void FlashBorder(string color)
    {
        startTime = Time.time;
        endTime = startTime + duration;
        switch (color)
        {
            case "damage":
                flashColor = redFlash;
                break;
            default:
                flashColor = transparent;
                break;
        }
    }
}