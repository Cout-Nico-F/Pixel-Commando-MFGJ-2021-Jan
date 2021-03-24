using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    private GameManager gm;
    private PlayerController player;
    private Enemy[] enemiesArray;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        enemiesArray = FindObjectsOfType<Enemy>();
        Debug.LogError("ENEMY AMMOUNT: " + enemiesArray.Length);
        switch (gm.Difficulty)
        {
            case 1:
                TweakPlayer(health: 200, lives: 5);    
                TweakEnemies(health_multiplicator: 0.65);
                break;
            case 2:
                break;
            case 3:
                TweakPlayer(health: 80, lives: 2);
                TweakEnemies(health_multiplicator: 1.65);
                break;
            default: 
                break;
        }
    }

    private void TweakEnemies(double health_multiplicator)
    {
        foreach (Enemy enemy in enemiesArray)
        {
            enemy.healthPoints = (int)(enemy.healthPoints * health_multiplicator);
        }
    }

    private void TweakPlayer(int health, int lives)
    {
        player.GetComponent<PlayerController>().maxHealthPoints = health;
        player.GetComponent<PlayerController>().lives = lives;
    }

}
