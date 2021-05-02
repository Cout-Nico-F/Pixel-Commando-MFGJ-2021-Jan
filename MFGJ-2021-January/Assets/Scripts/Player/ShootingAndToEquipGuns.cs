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

    public void Configuration(IGunnig gunnig)
    {
        _gunnig = gunnig;
        _hasCongigutator = true;
        gun = gunnig.GetFirstGun();
        _factoryGuns = new GunsFactory(Instantiate(gunsConfiguration));
        concurrentGun = _factoryGuns.Create(gun);
        
    }

    private void Update()
    {
        if (!_hasCongigutator) return;
        if (Input.GetButton("Fire1"))
        {
            concurrentGun.Shoot(_gunnig.GetShootPoint());
        }
    }
}