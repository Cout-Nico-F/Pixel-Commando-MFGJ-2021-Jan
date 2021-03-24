using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    private GameManager gm;
    private PlayerController player;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Debug.LogError("ENEMY AMMOUNT: " + enemies.Length);
        switch (gm.Difficulty)
        {
            case 1:
                player.GetComponent<PlayerController>().maxHealthPoints = 200;
                player.GetComponent<PlayerController>().lives = 5;
                foreach (Enemy enemy in enemies)
                {
                    enemy.healthPoints = 1;
                }
                break;
        }
    }
}
