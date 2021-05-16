using Player.Bellseboss;
using UnityEngine;

public interface IGun : IBulletsInterface
{
    void Shoot(GameObject shotPoint);
    Sprite GetSprite();
}