using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulleting : MonoBehaviour
{
   public float speed;
   public float lifeTime = 1;

   void Start()
   {
       Invoke("DestroyBullet", lifeTime);
   }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
