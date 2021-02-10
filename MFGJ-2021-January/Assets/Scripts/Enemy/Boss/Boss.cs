using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    [SerializeField]
    private bool isFlipped = false;

    [Header("Variables")]
    public float speed = 2.5f;
    public int healthPoints = 200;
    public int attackOneDamage = 0;
    public int attackTwoDamage = 0;
    public GameObject closeRangeBullet;
    public GameObject longRangeBullet;
    [SerializeField] GameObject explosiveRocket;
    public float shootRange;
    public float startTimeBtwShots;
    float timeBtwShots;
    [SerializeField] float explosiveRocketProcChance = 5f;
    public Transform gunShotPoint;
    public Transform rocketShotPoint;
    //Add more variables as u need

    [Header("Patrol Points")]
    public Transform[] patrolPoints;
    [SerializeField]
    int current;
    [SerializeField]
    int randomPoint;

    private void Update()
    {
        Invoke(nameof(Movement), 1f);
    }

    //Look at player -> Flip Boss
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Movement()
    {
        //Patrol AI
        if (this.transform.position != patrolPoints[current].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[current].position, speed * Time.deltaTime);
            //patrolPoints[current].position = new Vector3(patrolPoints[current].position.x, patrolPoints[current].position.y, 0);
        }
        else
        {
            //Get a random number -> random point
            if (randomPoint != current)
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
                current = randomPoint;
                //If the new numer is the same of the current number, get another one.
                if (randomPoint == current)
                {
                    randomPoint = Random.Range(0, patrolPoints.Length);
                    current = randomPoint;
                }
            }
            else
            {
                randomPoint = Random.Range(0, patrolPoints.Length);
                current = randomPoint;
            }

            //Get next point (in order of patrolPoitns list) 
            //current = (current + 1) % patrolPoints.Length;
        }
    }

    public void TryShoot()
    {
        bool playerInRange = false;
        if (player != null)
        {
            playerInRange = Vector2.Distance(transform.position, player.position) < shootRange;
        }

        if (timeBtwShots <= 0 && playerInRange)
        {
            if (Boss_Attack.attackNumber == 1)
            {
                Vector3 difference = rocketShotPoint.position - player.position;
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                rocketShotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ  -90) ;
                if (isFlipped)
                {
                    rocketShotPoint.rotation = Quaternion.Euler(0f, 0f, -rotZ);
                }

                Instantiate(closeRangeBullet, rocketShotPoint.position, rocketShotPoint.rotation);
                RandomThirdAttack();
            }
            else if (Boss_Attack.attackNumber == 2)
            {
                Instantiate(longRangeBullet, gunShotPoint.position, Quaternion.identity);
                //Lets manage the bullet size and lifetime in the bullet prefab (to be consistent) 
                RandomThirdAttack();
            }
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    //Take Damage
    public void TakeDamage(int damage)
    {
        Debug.Log("Boss is taking damage. Boss Life: " + healthPoints);
        //Take Player Damage
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            Death();
        }

        //Change SecondAttack if boss health < 50
        if (healthPoints < (healthPoints / 2))
        {
            //Increade Attacj=k Two Damage
            attackTwoDamage *= 2;
        }
    }

    #region States
    //CREATE FUCNTIONS OF BOSS ATTACKING
    public void FirstAttack()
    {
        //First 'gun' -> first attack

        TryShoot();

    }

    public void SecondAttack()
    {
        //Second 'gun' -> second attack
        TryShoot();
    }

    public void RandomThirdAttack()
    {
        float explosiveRocketProc = Random.Range(1, 100);
        if (explosiveRocketProc < explosiveRocketProcChance)
        {
            Instantiate(explosiveRocket, transform.position, Quaternion.identity);
        }
    }

    //CREATE BOSS DEATH FUNCTION -> DESTROY
    public void Death()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Destroy");
        Destroy(this.gameObject);
    }
    #endregion

    //Collision with bullets
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Take Damage
        if (collision.CompareTag("Bullet") || collision.CompareTag("Explosion"))
        {
            TakeDamage(collision.GetComponent<Bulleting>().damageToBoss);
        }
        if (collision.gameObject.name == "Rocket_Blue(Clone)")
        {
            TakeDamage(collision.GetComponent<Bulleting>().damageToBoss);//instead of multiplying for 10 here, lets set 200 on rocket bulleting damageToBoss
        }
    }

}
