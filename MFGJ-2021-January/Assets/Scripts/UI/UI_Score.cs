using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    // Start is called before the first frame update

    public void SetScore(string score)
    {
        GetComponentInChildren<Text>().text = score.ToString();
    }
}
