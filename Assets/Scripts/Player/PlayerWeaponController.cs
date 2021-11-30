using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWeaponController : MonoBehaviour
{
    public PlayerStats playerStats;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI ammoUsageText;

    public PlayerMovementController playerMovement;

    public int ammoBoxAmount;

    public int ammo;

    public Gun[] guns;
    private uint m_currentGun = 0;

    private void Start()
    {
        RefreshGUI();
        guns[m_currentGun].RefreshAmmoGui(ammoText, ammo);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            guns[m_currentGun].gameObject.SetActive(false);
            m_currentGun = (m_currentGun + 1) % (uint)guns.Length;
            guns[m_currentGun].gameObject.SetActive(true);
            RefreshGUI();
        }

        if(Input.GetMouseButtonDown(0))
        {
            guns[m_currentGun].StartShooting(ammoText, ref ammo);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            guns[m_currentGun].StopShooting();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ammo"))
        {
            playerMovement.PlaySound(playerMovement.playerStats.ammoPickup);
            ammo = Math.Min(ammo + ammoBoxAmount, playerStats.maxAmmo);
            guns[m_currentGun].RefreshAmmoGui(ammoText, ammo);
            Destroy(other.gameObject);
        }
    }

    private void RefreshGUI()
    {
        ammoUsageText.text = string.Format("({0})", guns[m_currentGun].ammoUsage);
    }
}
