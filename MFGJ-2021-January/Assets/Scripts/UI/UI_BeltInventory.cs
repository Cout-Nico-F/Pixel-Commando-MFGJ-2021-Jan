using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BeltInventory : MonoBehaviour
{
    // Start is called before the first frame update
    #region Primary Weapons Belt Definitions
    public GameObject primaryWeaponBox, secondaryWeaponBox, ammoUI, rocketLaunchable, javelinLaunchable;
    public Sprite defaultGunSprite;
    private Text ammoText;
    private Image weaponImage;
    public Transform activeLaunchableTransform, secondaryLaunchableTransform;
    #endregion

    #region Items Belt Definitions
    public GameObject item1, item1End, item2, bombsActive, bombsInactive;
    #endregion

    #region UI Effects
    public GameObject pickupAnimationPrefab;
    public Camera mainCamera;
    //private UI_PickupAnimation uiPickupAnimation;
    #endregion

    private void Awake() {
        ammoText = ammoUI.GetComponentInChildren<Text>();
        weaponImage = primaryWeaponBox.GetComponentInChildren<Image>();
        mainCamera = Camera.main;
    }

    #region Belt Functions
    public void SetAmmo(string ammo){
        ammoText.text = ammo;
        if (ammo == "0")
        {
            ammoText.text = "∞";
            SetPrimaryWeaponImage(defaultGunSprite);
        }
    }
    
    public void SetPrimaryWeaponImage(Sprite newWeaponSprite){
        weaponImage.sprite = newWeaponSprite;
    }

    public void SwapLaunchables(){
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

    public void TriggerPickupAnimation(GameObject item)
    {
        // Object properties
        Sprite itemSprite = item.GetComponent<SpriteRenderer>().sprite;
        Vector3 itemPosition = item.transform.position;
        Vector3 itemScale = item.transform.localScale;
        Quaternion itemRotation = item.transform.rotation;

        // Instantiate object
        GameObject _pickUpItem = Instantiate(pickupAnimationPrefab, itemPosition, itemRotation);

        // Make it smaller, use the picked up object's sprite
        _pickUpItem.transform.localScale = itemScale * 0.8f;
        _pickUpItem.GetComponent<SpriteRenderer>().sprite = itemSprite;
        UI_PickupAnimation uiPickupAnimation = _pickUpItem.GetComponent<UI_PickupAnimation>();

        Vector3 pickupItemDestination = new Vector3(-10f, -10f, 0f);
        // Set the animation destination depending on what was picked up
        if (item.CompareTag("Gun"))
        {
            pickupItemDestination = mainCamera.ScreenToWorldPoint(primaryWeaponBox.transform.position);
        }
        if (item.name.Contains("WireCutter"))
        {
            pickupItemDestination = mainCamera.ScreenToWorldPoint(item1.transform.position);
        }
        if (item.name.Contains("Bombs"))
        {
            pickupItemDestination = mainCamera.ScreenToWorldPoint(item2.transform.position);
        }

        // TODO Fix destination. The current destination varies grealy depending on the camera position
        // despite using ScreenToWorldPoint.

        uiPickupAnimation.MoveItem(pickupItemDestination);
    }
}
