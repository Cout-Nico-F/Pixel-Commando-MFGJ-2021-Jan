using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject primaryWeaponBox, secondaryWeaponBox, ammoUI, livesUI, scoreUI, player;
    public Sprite defaultGunSprite;
    private Text ammoText;
    private Image weaponImage;
    

    private void Awake() {
        ammoText = ammoUI.GetComponentInChildren<Text>();
        weaponImage = primaryWeaponBox.GetComponentInChildren<Image>();
    }
    
    public void setAmmo(string ammo){
        ammoText.text = ammo;
        if (ammo == "0")
        {
            ammoText.text = "∞";
            setPrimaryWeaponImage(defaultGunSprite);
        }
    }

    public void setPrimaryWeaponImage(Sprite newWeaponSprite){
        //Sprite playerWeaponSprite = GameObject.FindGameObjectWithTag("playerWeaponImage").GetComponent<SpriteRenderer>().sprite;
        //Debug.Log(GameObject.FindGameObjectWithTag("playerWeaponImage").GetComponent<SpriteRenderer>().name);
        weaponImage.sprite = newWeaponSprite;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
