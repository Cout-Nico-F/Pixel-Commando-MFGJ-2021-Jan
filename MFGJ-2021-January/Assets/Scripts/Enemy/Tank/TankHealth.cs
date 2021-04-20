using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    internal Tank tankScript;

    int TakeDamage(int damage)
    {
        return tankScript.healthPoints - damage;
    }

}
