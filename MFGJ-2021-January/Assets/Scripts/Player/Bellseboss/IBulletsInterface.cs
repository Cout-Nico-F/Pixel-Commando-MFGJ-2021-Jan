using System;
using UnityEngine;

namespace Player.Bellseboss
{
    public interface IBulletsInterface
    {
        event Action<int> OnBulletUpdate;
    }
}