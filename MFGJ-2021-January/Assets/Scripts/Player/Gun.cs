using UnityEngine;

public abstract class Gun : MonoBehaviour, IGun
{
    [SerializeField] protected string id;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce;
    [SerializeField] float cameraShakeDuration;
    [SerializeField] private float cameraShakeAmount;

    public string Id => id;
    public void Shoot(GameObject shotPoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, shotPoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.transform.up * bulletForce, ForceMode2D.Impulse);

        // Shake the camera for (duration, amount)
        CameraShake.Shake(cameraShakeDuration, cameraShakeAmount);
    }
}