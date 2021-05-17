using Player.Bellseboss;
using UnityEngine;

public interface IGun : IBulletsInterface
{
    void Shoot(GameObject shotPoint);
    Sprite GetSprite();
    int GetBulletCount();
    string GetId();
    void AddBullets(int BulletCount);
}