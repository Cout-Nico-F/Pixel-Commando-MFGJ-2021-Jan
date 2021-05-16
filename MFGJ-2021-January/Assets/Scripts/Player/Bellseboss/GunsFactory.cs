using UnityEngine;

public class GunsFactory
{
    private readonly GunsConfiguration gunsConfiguration;

    public GunsFactory(GunsConfiguration gunssConfiguration)
    {
        this.gunsConfiguration = gunssConfiguration;
    }
        
    public Gun Create(string id)
    {
        var prefab = gunsConfiguration.GetGunPrefabById(id);

        return Object.Instantiate(prefab);
    }
}