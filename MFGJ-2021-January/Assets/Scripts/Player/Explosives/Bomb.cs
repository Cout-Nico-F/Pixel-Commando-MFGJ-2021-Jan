using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour , IExplode //mono is needed here?
{
    public void Detonate()
    {
        //Bomb cant be detonated. Its timer based.
    }

    public void Plant()
    {
        //Instantiate a bomb prefab in front of the player
        //bomb prefab will contain animation and sound
        //bomb prefab has a timer, then explodes.
    }
}
