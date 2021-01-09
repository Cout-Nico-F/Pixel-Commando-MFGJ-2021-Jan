using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulleting : MonoBehaviour
{
   public float speed;
   public float lifeTime;

   void Start()
   {
       Invoke("DestroyBullet", lifeTime);
   }

   void Update()
   {
       
   }
}
