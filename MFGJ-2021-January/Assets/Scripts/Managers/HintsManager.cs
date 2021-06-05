using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour
{
    private string h0_start =      "Break through their defenses to the north, then go east. Locate the fence and breach it!";
    private string h1_holdFire =   "Hold Fire to Auto-Shoot!";
    private string h2_move =       "Keep moving to avoid being shot!";
    private string h3_intelSouth = "Intel: you may find rocket ammo supplies south of your location!";
    private string h4_Score =      "+1up from score!";
    private string h5_rush =       "Play smart, rushing only leads to failure!";
    private string h6 =            "Using rockets against Machine Gunners is a great idea!";
    private string h7 =            "You will find the enemy base further east. Keep going!";
    private string h8 =            "There's the fence guarding the base! Secure your position, then breach it!";
    private string h9 =            "Intel: you may find tools in the yellow mark of your map, look out for wirecutters there";
    private string h10 =           "The enemies are building rocket launchers in the jungle. Look out for ammo!";
    private string h11 =           "Congrats! Next update March-2021, Follow us in itch.io to be the first to play Lvl 2!";//unused right now
    private string h12 =           "Enemy workshops ahead: you can find a tool to cut the wire fence here";
    private string h13 =           "Press F to USE the wirecutter (found at the yellow marker on the map)";
    private string h14 =           "A sturdy gate! Find explosives and blow it up!! Look inside wooden houses for bombs";
    
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
        name = name.ToLower();
        switch (name)
        {
            case "start":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h0_start;
                break;
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
            case "rush":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h5_rush;
                break;
            case "mg":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h6;
                break;
            case "find":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h7;
                break;
            case "fence":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h8;
                break;
            case "breach":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h9;
                break;
            case "making":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h10;
                break;
            case "end":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h11;
                break;
            case "workshops":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h12;
                break;
            case "pressf":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h13;
                break;
            case "findbombs":
                hintsPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = h14;
                break;
            default:
                Debug.LogWarning("Name mismatch on HintsManager line 38 !! Name:"+name);
                break;
        }
    }
    public void ShowHintPanel(string name, float time)
    {
        ChangeHint(name);
        hintsPanel.SetActive(true);
        isActive = true;
        timer = time;
    }
}
