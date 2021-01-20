using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulleting : MonoBehaviour
{
    public float lifeTime = 0.3f;
    public int damage = 10;

    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
