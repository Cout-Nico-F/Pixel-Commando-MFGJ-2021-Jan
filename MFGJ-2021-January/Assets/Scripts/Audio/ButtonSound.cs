using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    AudioManager audioManager;
    Button thisButton;
    private bool m_MouseOver = false;

    public WeaponSounds mouseHoverSound = new WeaponSounds();
    public WeaponSounds mouseClickSound = new WeaponSounds();
    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        thisButton = GetComponent<Button>();
    }

    private void Update()
    {
        if(m_MouseOver)
        {
            OnMouseHover();
            m_MouseOver = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_MouseOver = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_MouseOver = false;
    }

   public void OnPointerClick(PointerEventData eventData)
   {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnMouseClick();
            Debug.Log("click");
        }

    }


    private void OnMouseHover()
    {
        switch (mouseHoverSound)
        {
            case WeaponSounds.EnemyBulletSound:
                audioManager.PlaySound("Damage");
                break;
            case WeaponSounds.McBulletSound:
                audioManager.PlaySound("BulletSound");
                break;
            case WeaponSounds.RocketSounds:
                audioManager.PlaySound("RocketFire");
                audioManager.PlaySound("RocketTrust");
                break;
            case WeaponSounds.SpearSounds:
                audioManager.PlaySound("TrowSpear");
                break;
            case WeaponSounds.RapidFireSound:
                audioManager.PlaySound("RapidFire");
                break;
            case WeaponSounds.Bomb:
                audioManager.PlaySound("FallingBomb");
                break;
            case WeaponSounds.BossLvl1Explode:
                audioManager.PlaySound("BossExplode");
                break;
            case WeaponSounds.Skar:
                audioManager.PlaySound("Skar");
                break;
            case WeaponSounds.LoadWeapon:
                audioManager.PlaySound("PickUpWeapon");
                break;
        }
        if (Input.GetMouseButton(0))
        {
            OnMouseClick();
        }
    }

    private void OnMouseClick()
    {
        switch (mouseClickSound)
        {
            case WeaponSounds.EnemyBulletSound:
                audioManager.PlaySound("Damage");
                break;
            case WeaponSounds.McBulletSound:
                audioManager.PlaySound("BulletSound");
                break;
            case WeaponSounds.RocketFire:
                audioManager.PlaySound("RocketFire");
                break;
            case WeaponSounds.SpearSounds:
                audioManager.PlaySound("TrowSpear");
                break;
            case WeaponSounds.RapidFireSound:
                audioManager.PlaySound("RapidFire");
                break;
            case WeaponSounds.Splat:
                audioManager.PlaySound("Splat");
                break;
            case WeaponSounds.BossLvl1Explode:
                audioManager.PlaySound("BossExplode");
                break;
            case WeaponSounds.Skar:
                audioManager.PlaySound("Skar");
                break;
            case WeaponSounds.LoadWeapon:
                audioManager.PlaySound("PickUpWeapon");
                break;
            case WeaponSounds.DestroyHut:
                audioManager.PlaySound("DestroyHut");
                break;
            case WeaponSounds.TrowSpear:
                audioManager.PlaySound("TrowSpear");
                break;
        }
    }


}
