using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeNextLevel : MonoBehaviour
{
    
    private void Start()
    {
        Invoke(nameof(Slow), 0.8f);
        Invoke(nameof(DelegateNextLevel), 1.6f);
    }
     private void Slow()
    {
        Time.timeScale = 0.2f;
    }
    private void DelegateNextLevel()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
        Time.timeScale = 1;
        lm.NextLevel();
    }  
}
