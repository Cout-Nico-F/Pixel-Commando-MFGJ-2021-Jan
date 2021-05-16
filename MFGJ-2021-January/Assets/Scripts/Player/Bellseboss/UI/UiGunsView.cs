using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player.Bellseboss.UI
{
    public class UiGunsView : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Image gunView;
        [SerializeField] private Text bulletCounts;

        private void Start()
        {
            playerController.GetGunsAndEquips().OnBulletUpdateImage += OnOnBulletUpdateImage;
        }

        private void OnOnBulletUpdate(int countBullet)
        {
            Debug.Log("Fue llamado porque disparo");
            if (countBullet < 0)
            {
                bulletCounts.text = "∞";
                return;
            }
            bulletCounts.text = $"{countBullet}";
        }

        private void OnOnBulletUpdateImage(Sprite newSprite, IGun newGun)
        {
            Debug.Log("Fue llamado porque cambio de arma");
            gunView.sprite = newSprite;
            newGun.OnBulletUpdate += OnOnBulletUpdate;
        }
    }
}