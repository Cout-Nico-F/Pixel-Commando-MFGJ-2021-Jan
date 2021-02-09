using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullets : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 0;
    
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * projectileSpeed);
    }
}
