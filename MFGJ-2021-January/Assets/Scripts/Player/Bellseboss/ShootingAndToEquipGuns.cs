using System;
using UnityEngine;

public class ShootingAndToEquipGuns : MonoBehaviour
{
    private IGunnig _gunnig;
    private bool _hasCongigutator;
    private string gun;
    private IGun concurrentGun;
    [SerializeField] private GunsConfiguration gunsConfiguration;
    private GunsFactory _factoryGuns;
    public event Action<Sprite, IGun> OnBulletUpdateImage;
    
    public void Configuration(IGunnig gunnig)
    {
        _gunnig = gunnig;
        _hasCongigutator = true;
        gun = gunnig.GetFirstGun();
        _factoryGuns = new GunsFactory(Instantiate(gunsConfiguration));
        EquipTheFirstGun();
    }

    private void EquipTheFirstGun()
    {
        concurrentGun = _factoryGuns.Create(gun);
        OnBulletUpdateImage?.Invoke(concurrentGun.GetSprite(), concurrentGun);
    }

    private void Update()
    {
        if (!_hasCongigutator) return;
        if (Input.GetButton("Fire1"))
        {
            try
            {
                concurrentGun.Shoot(_gunnig.GetShootPoint());
            }
            catch (Exception)
            {
                EquipTheFirstGun();                
            }
        }
    }

    public void EquipNewGun(string nameOfGun)
    {
        var bulletsAditionals = 0;
        if (concurrentGun.GetId().Contains(nameOfGun))
        {
            bulletsAditionals = concurrentGun.GetBulletCount();
        }
        concurrentGun = _factoryGuns.Create(nameOfGun);
        concurrentGun.AddBullets(bulletsAditionals);
        OnBulletUpdateImage?.Invoke(concurrentGun.GetSprite(), concurrentGun);
    }
}