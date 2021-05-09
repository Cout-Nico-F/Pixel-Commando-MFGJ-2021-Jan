using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/GunConfiguration")]
public class GunsConfiguration : ScriptableObject
{
    [SerializeField] private Gun[] guns;
    private Dictionary<string, Gun> idToGun;

    private void Awake()
    {
        idToGun = new Dictionary<string, Gun>(guns.Length);
        foreach (var gun in guns)
        {
            idToGun.Add(gun.Id, gun);
        }
    }

    public Gun GetGunPrefabById(string id)
    {
        if (!idToGun.TryGetValue(id, out var gun))
        {
            throw new Exception($"PowerUp with id {id} does not exit");
        }
        return gun;
    }
}