using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    Enemy enemy;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.healthPoints <= 0)
        {
            //remove Roof
            Debug.Log("**************REMOVING ROOFS******************");
            //Activate enemies inside
        }
    }

}
