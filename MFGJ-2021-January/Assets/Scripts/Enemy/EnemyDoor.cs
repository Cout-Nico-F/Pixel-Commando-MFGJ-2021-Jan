using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    Enemy enemy;

    [SerializeField]
    GameObject roof;

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
            if (roof.activeSelf)
            {
                roof.SetActive(false);
            }
            //Activate enemies inside
        }
    }

}
