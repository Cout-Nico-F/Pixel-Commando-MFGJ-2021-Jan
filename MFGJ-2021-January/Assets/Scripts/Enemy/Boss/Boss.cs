using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    [SerializeField]
    private bool isFlipped = false;

    [Header("Variables")]
    public int healthPoints = 200;
    public int attackOneDamage = 0;
    public int attackTwoDamage = 0;
    //Add more variables as u need

    //Look at player -> Flip Boss
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x < player.position.x && isFlipped)
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

    //Take Damage
    public void TakeDamage(int damage)
    {
        Debug.Log("Boss is taking damage. Boss Life: " + healthPoints);
        //Take Player Damage
        healthPoints -= damage;

        if(healthPoints <= 0)
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
    }

    public void SecondAttack()
    {
        //Second 'gun' -> second attack
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
        if(collision.gameObject.name == "Rocket_Blue(Clone)")
        {
            TakeDamage(collision.GetComponent<Bulleting>().damageToBoss * 10);
        }
    }

}
