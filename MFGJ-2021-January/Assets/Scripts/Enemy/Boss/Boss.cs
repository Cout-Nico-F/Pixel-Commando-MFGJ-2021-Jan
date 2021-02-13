using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    #region Vriables
    public Transform player;
    [SerializeField]
    private bool isFlipped = false;

    [Header("Variables")]
    public float speed = 2.5f;
    
    public int healthPoints;
    public int maxHealth = 600;
    public int attackOneDamage = 0;
    public int attackTwoDamage = 0;
    public GameObject closeRangeBullet;
    public GameObject longRangeBullet;
    [SerializeField] GameObject explosiveRocket;
    public float shootRange;
    public float startTimeBtwShots;
    float timeBtwShots;
    [HideInInspector]
    public float explosiveRocketProcChance = 5f;
    public Transform gunShotPoint;
    public Transform rocketShotPoint;
    public Animation hitAnimation;
    //Add more variables as u need

    bool isRepeat = false;
    bool isRepeat2 = false;

    public BossZoneColliders bossZoneCol1;
    public BossZoneColliders bossZoneCol2;

    [Header("Patrol Points")]
    public int bossZone = 0;
    [SerializeField]
    private int pointsPerZone = 5;
    [SerializeField]
    private int lastPointsNumber = 0;
    public Transform[] patrolPoints;
    [SerializeField]
    int current;
    [SerializeField]
    int randomPoint;
    #endregion

    private void Awake()
    {
        healthPoints = maxHealth;
        hitAnimation = GetComponent<Animation>();
    }
    private void Update()
    {
        Invoke(nameof(Movement), 1f);
    }

    #region Boss Methods
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
    //Boss Movement
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
                #region Random Point per Zone
                switch(bossZone)
                {
                    case 0:
                        Debug.Log("0");
                        pointsPerZone = 5;
                        lastPointsNumber = 0;
                        randomPoint = Random.Range(lastPointsNumber, pointsPerZone); //0->5.
                        break;
                    case 1:
                        Debug.Log("1");
                        pointsPerZone = 11;
                        lastPointsNumber = 6;
                        randomPoint = Random.Range(lastPointsNumber, pointsPerZone);//6->11.
                        
                        break;
                    case 2:
                        Debug.Log("2");
                        pointsPerZone = 17;
                        lastPointsNumber = 12;
                        randomPoint = Random.Range(lastPointsNumber, pointsPerZone);//12->17
                        break;
                }
                #endregion
                current = randomPoint;
            }
            else
            {
                #region Random Point per Zone
                switch (bossZone)
                {
                    case 0:
                        Debug.Log("0");
                        pointsPerZone = 5;
                        lastPointsNumber = 0;
                        randomPoint = Random.Range(lastPointsNumber, pointsPerZone); //0->5.
                        break;
                    case 1:
                        Debug.Log("1");
                        pointsPerZone = 11;
                        lastPointsNumber = 6;
                        randomPoint = Random.Range(lastPointsNumber, pointsPerZone);//6->11.

                        break;
                    case 2:
                        Debug.Log("2");
                        pointsPerZone = 17;
                        lastPointsNumber = 12;
                        randomPoint = Random.Range(lastPointsNumber, pointsPerZone);//12->17
                        break;
                }
                #endregion
            }

            //Get next point (in order of patrolPoitns list) 
            //current = (current + 1) % patrolPoints.Length;
        }
    }
    //Boss Shooting
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

                rocketShotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ -90) ;
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
        //Take Player Damage
        healthPoints -= damage;

        if (hitAnimation != null)
        {
            hitAnimation.Play();
        }
        if (healthPoints <= 0)
        {
            Death();
        }

        if (healthPoints <= maxHealth  * 0.4 && isRepeat == false)
        {
            //Increase speed
            speed = 7;

            //Increade Attack Two Damage
            attackOneDamage *= 2;

            //Next zone
            bossZone += 1;
            isRepeat = true;
            bossZoneCol2.ToggleColliderColorandState();
        }
        else if (healthPoints <= maxHealth *0.7 && isRepeat2 == false)
        {
            //Increase speed
            speed = 7;

            //Increade Attack One Damage
            attackOneDamage *= 2;

            //Next zone
            bossZone += 1;
            isRepeat2 = true;
            bossZoneCol1.ToggleColliderColorandState();
        }
    }
    #endregion

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
            Vector3 difference = rocketShotPoint.position - player.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            rocketShotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
            if (isFlipped)
            {
                rocketShotPoint.rotation = Quaternion.Euler(0f, 0f, -rotZ);
            }

            Instantiate(explosiveRocket, rocketShotPoint.position, rocketShotPoint.rotation);
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
