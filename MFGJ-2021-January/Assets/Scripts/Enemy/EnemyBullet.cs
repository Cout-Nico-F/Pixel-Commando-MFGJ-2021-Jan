using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    float hittingChance = 0.82f;
    Vector2 target;


    // Start is called before the first frame update
    void Start()
    {
        AimRoll();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void AimRoll()
    {
        float luck = Random.Range(0.1f, 1f);

        if (luck <= hittingChance / 100)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
            return;
        }
        else
        {
            float badAim_X = Random.Range(0f, 0.67f);
            float badAim_Y = Random.Range(0f, 0.67f);
            try
            {
                target = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(badAim_X, badAim_Y);
            }
            catch
            {
                Debug.Log("No player found in the scene");
            }
        }
    }
}
