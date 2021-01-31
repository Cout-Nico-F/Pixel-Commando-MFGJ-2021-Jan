using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour
{
    private string h1_holdFire = "Hold Fire to Auto-Shoot";
    private string h2_move = "Keep moving to avoid being shot!";
    private string h3_intelSouth = "Intel: you may find rocket ammo supplies south of your location!";
    private string h4_Score = "Get 4000 points or more to unlock extra lives!";
    //private string h5_

    private bool isActive;

    public GameObject hintsPanel;

    private float timer;

    private void Awake()
    {
        timer = 0;
        isActive = false;
    }
    private void Update()
    {
        if (timer < 0)
        {
            timer = 0;
            hintsPanel.SetActive(false);
            isActive = false;
        }
        if (isActive)
        {
            timer -= Time.deltaTime;
        }
    }
    public void ChangeHint(string name)
    {
        name.ToLower();
        switch (name)
        {
            case "holdfire":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h1_holdFire;
                break;
            case "move":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h2_move;
                break;
            case "intelsouth":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h3_intelSouth;
                break;
            case "score":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h4_Score;
                break;
            default:
                Debug.LogWarning("Name mismatch on HintsManager line 38 !!");
                break;
        }
    }
    public void ShowMessage(string name, float time)
    {
        ChangeHint(name);
        hintsPanel.SetActive(true);
        isActive = true;
        timer = time;
    }
}
