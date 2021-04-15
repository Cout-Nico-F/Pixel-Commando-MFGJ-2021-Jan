using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosives : MonoBehaviour
{
    private bool hasBombs = false;

    private bool bombIsPlanted = false;

    private IExplode explosive;

    public bool HasBombs { get => hasBombs; set => hasBombs = value; }
    public bool BombIsPlanted { get => bombIsPlanted; set => bombIsPlanted = value; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (bombIsPlanted)
            {
                explosive.Detonate();
                bombIsPlanted = false;
            }
            else
            {
                if (hasBombs) //hasbombs turn true when you pick up a bomb-Item
                {
                    explosive.Plant();
                    bombIsPlanted = true;
                    hasBombs = false;
                }
                else //has no ammo
                {
                    //Shake or Animate the Bomb UI square
                }
            }
        }
    }
}
