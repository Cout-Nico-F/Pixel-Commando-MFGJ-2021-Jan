using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        GameManager gm = FindObjectOfType<GameManager>();

        //Delete File/Reset Data to create new one in next level (On Game Manager script).
        string a_FileContents = "";
        PlayerPrefs.SetString("Data Saved", a_FileContents);
        var fullPath = Path.Combine(Application.persistentDataPath, gm.dataFileName);
        File.Delete(fullPath);

        Time.timeScale = 1;
        gm.NextLevel();
    } 
}
