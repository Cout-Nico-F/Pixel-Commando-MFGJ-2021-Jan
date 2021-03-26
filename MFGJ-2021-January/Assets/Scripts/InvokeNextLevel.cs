using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InvokeNextLevel : MonoBehaviour
{
    AudioManager audioManager;
   
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        Invoke(nameof(Slow), 0.8f);
        //Invoke(nameof(DelegateNextLevel), audioManager.winMxLength);
        StartCoroutine(ChangeLevel());
    }

    private void Slow()
    {
        Time.timeScale = 0.2f;
    }

    private void DelegateNextLevel()
    {
        LevelManager lm = FindObjectOfType<LevelManager>();
      
        //    //Delete File/Reset Data to create new one in next level (On Game Manager script).
        //#if UNITY_WEBGL
        //    string a_FileContents = "";
        //    PlayerPrefs.SetString("Data Saved", a_FileContents);
        //#endif
        //#if UNITY_STANDALONE
        //    var fullPath = Path.Combine(Application.persistentDataPath, "PixelCommando.dat");
        //    File.Delete(fullPath);
        //#endif

        Time.timeScale = 1;
        lm.NextLevel();
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSecondsRealtime(audioManager.musicAudiosource.clip.length);
        DelegateNextLevel();   
    }
}
