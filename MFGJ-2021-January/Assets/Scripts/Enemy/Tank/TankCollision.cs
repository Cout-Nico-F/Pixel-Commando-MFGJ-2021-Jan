using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCollision : MonoBehaviour
{
    [SerializeField] internal Tank tankScript;

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("InfantryEnemy") || col.gameObject.CompareTag("MachinegunEnemy"))
        {
            col.gameObject.GetComponent<Enemy>().Die();
        }
    }

}
