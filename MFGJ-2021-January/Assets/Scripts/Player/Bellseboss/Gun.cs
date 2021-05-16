using System;
using UnityEngine;

public abstract class Gun : MonoBehaviour, IGun
{
    [SerializeField] protected string id;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Sprite gunSprite;
    [SerializeField] protected int bulletsCounts;
    [SerializeField] private float bulletForce;
    [SerializeField] private float cameraShakeDuration;
    [SerializeField] private float cameraShakeAmount;
    [SerializeField]private float cooldown;
    private float _nextFire;

    public string Id => id;
    public virtual void Shoot(GameObject shotPoint)
    {
        if (!(Time.time > _nextFire)) return;
        _nextFire = Time.time + cooldown;
        if (bulletsCounts == 0)
        {
            //Hacer sonido de estar vacio
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, shotPoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.transform.up * bulletForce, ForceMode2D.Impulse);
        if (bulletsCounts != -1)
        {
            bulletsCounts--;
        }
        OnBulletUpdate?.Invoke(bulletsCounts);
        // Shake the camera for (duration, amount)
        CameraShake.Shake(cameraShakeDuration, cameraShakeAmount);
    }

    public Sprite GetSprite()
    {
        return gunSprite;
    }

    public event Action<int> OnBulletUpdate;
    public event Action<Sprite> OnBulletUpdateImage;
}