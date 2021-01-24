using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Briefing");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartMission()
    {
        SceneManager.LoadScene("Level One");
    }
}
