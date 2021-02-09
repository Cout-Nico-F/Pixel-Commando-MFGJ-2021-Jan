using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    [SerializeField] float lifeTime = 1f;
    [SerializeField] int damage = 20;
    [SerializeField] float speed = 0f;
    Vector2 target;
    [SerializeField] GameObject projectilePrefab;
    void Start()
    {
        GetTarget();
        Invoke("Explosion", lifeTime);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }

    void GetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Explosion()
    {
        Instantiate(projectilePrefab, transform.position, transform.rotation);
        var rotation1 = new Quaternion();
        rotation1.eulerAngles = new Vector3(0, 0, 90);
        Instantiate(projectilePrefab, transform.position, rotation1);
        var rotation2 = new Quaternion();
        rotation2.eulerAngles = new Vector3(0, 0, 270);
        Instantiate(projectilePrefab, transform.position, rotation2);
        var rotation3 = new Quaternion();
        rotation3.eulerAngles = new Vector3(0, 0, 180);
        Instantiate(projectilePrefab, transform.position, rotation3);
        var rotation4 = new Quaternion();
        rotation4.eulerAngles = new Vector3(0, 0, 45);
        Instantiate(projectilePrefab, transform.position, rotation4);
        var rotation5 = new Quaternion();
        rotation5.eulerAngles = new Vector3(0, 0, 135);
        Instantiate(projectilePrefab, transform.position, rotation5);
        var rotation6 = new Quaternion();
        rotation6.eulerAngles = new Vector3(0, 0, 225);
        Instantiate(projectilePrefab, transform.position, rotation6);
        var rotation7 = new Quaternion();
        rotation7.eulerAngles = new Vector3(0, 0, 315);
        Instantiate(projectilePrefab, transform.position, rotation7);

        Destroy(gameObject);
    }
}
