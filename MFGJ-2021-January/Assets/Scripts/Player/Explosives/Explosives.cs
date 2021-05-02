using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosives 
{
    private bool hasBombs = false;

    private bool bombIsPlanted = false;

    private IExplode explosive;

    public bool HasBombs { get => hasBombs; set => hasBombs = value; }
    public bool BombIsPlanted { get => bombIsPlanted; set => bombIsPlanted = value; }
    public IExplode Explosive { get => explosive; set => explosive = value; }

    public void Update()
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
                    //the bomb ui square has to appear when you pick your first bomb, avoiding this ui to shake when you use wirecutters
                }
            }
        }
    }
}
