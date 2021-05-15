using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BeltInventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Primary Weapons Belt Definitions
    public GameObject primaryWeaponBox, secondaryWeaponBox, ammoUI, livesUI, scoreUI, player, rocketLaunchable, javelinLaunchable;
    public Sprite defaultGunSprite;
    private Text ammoText;
    private Image weaponImage;
    public Transform activeLaunchableTransform, secondaryLaunchableTransform;
    #endregion

    #region Items Belt Definitions
    public GameObject item1, item1End, item2, bombsActive, bombsInactive;
    #endregion

    private void Awake() {
        #region Main weapons
        ammoText = ammoUI.GetComponentInChildren<Text>();
        weaponImage = primaryWeaponBox.GetComponentInChildren<Image>();
        #endregion
    }
    
    #region Belt Functions
    public void setAmmo(string ammo){
        ammoText.text = ammo;
        if (ammo == "0")
        {
            ammoText.text = "∞";
            setPrimaryWeaponImage(defaultGunSprite);
        }
    }
    
    public void setPrimaryWeaponImage(Sprite newWeaponSprite){
        weaponImage.sprite = newWeaponSprite;
    }

    public void swapLaunchables(){
        Transform rTransform = rocketLaunchable.GetComponent<Transform>();
        Transform jTransform = javelinLaunchable.GetComponent<Transform>();
        
        //Because of how Unity handles floats, you can't compare with equals
        //Used the < workaround: if x is lower (on the left), it means it's active.
        if (rTransform.position.x < javelinLaunchable.GetComponent<Transform>().position.x){
            rTransform.position = secondaryLaunchableTransform.position;
            rTransform.localScale = secondaryLaunchableTransform.localScale;
            jTransform.position = activeLaunchableTransform.position;
            jTransform.localScale = activeLaunchableTransform.localScale;
        }else{
            rTransform.position = activeLaunchableTransform.position;
            rTransform.localScale = activeLaunchableTransform.localScale;
            jTransform.position = secondaryLaunchableTransform.position;
            jTransform.localScale = secondaryLaunchableTransform.localScale;
        }
    }
    #endregion

    public void EnableUIItem(string item)
    {
        if (item == "Wirecutter")
        {
            item1.SetActive(true);
        }
        if (item == "Bombs")
        {
            item1End.SetActive(false);
            item2.SetActive(true);
        }
    }

    public void HasBombsUI(bool hasBombs)
    {
        if (hasBombs)
        {
            bombsActive.SetActive(true);
            bombsInactive.SetActive(false);
        }
        else
        {
            bombsInactive.SetActive(true);
            bombsActive.SetActive(false);
        }
    }
}
