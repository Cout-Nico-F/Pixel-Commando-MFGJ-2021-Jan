using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    public GameObject scoreUI;

    public void SetScore(string score)
    {
        GetComponentInChildren<Text>().text = score.ToString();
    }

    //TODO: Handle score from this script
}
