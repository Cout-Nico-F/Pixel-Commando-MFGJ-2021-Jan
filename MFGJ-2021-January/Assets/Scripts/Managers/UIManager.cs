using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject primaryWeaponBox, secondaryWeaponBox, ammoUI, livesUI, scoreUI, player, rocketLaunchable, javelinLaunchable;
    public Sprite defaultGunSprite;
    private Text ammoText;
    private Image weaponImage;

    private Transform activeLaunchableTransform, secondaryLaunchableTransform;
    
    

    private void Awake() {
        #region Main weapons
        ammoText = ammoUI.GetComponentInChildren<Text>();
        weaponImage = primaryWeaponBox.GetComponentInChildren<Image>();

        activeLaunchableTransform = rocketLaunchable.GetComponent<Transform>();
        secondaryLaunchableTransform = javelinLaunchable.GetComponent<Transform>();

        #endregion
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
        weaponImage.sprite = newWeaponSprite;
    }

    public void swapLaunchables(){
        
        Transform rTransform = rocketLaunchable.GetComponent<Transform>();
        Transform jTransform = javelinLaunchable.GetComponent<Transform>();

        if (rTransform.position == activeLaunchableTransform.position){
            Debug.Log("changed to javelin");
            rTransform.position = secondaryLaunchableTransform.position;
            rTransform.localScale = secondaryLaunchableTransform.localScale;
            jTransform.position = activeLaunchableTransform.position;
            jTransform.localScale = activeLaunchableTransform.localScale;
        }else{
            Debug.Log("changed to rocket");
            rTransform.position = activeLaunchableTransform.position;
            rTransform.localScale = activeLaunchableTransform.localScale;
            jTransform.position = secondaryLaunchableTransform.position;
            jTransform.localScale = secondaryLaunchableTransform.localScale;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
