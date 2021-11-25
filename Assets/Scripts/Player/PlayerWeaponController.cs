using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWeaponController : MonoBehaviour
{
    public PlayerStats playerStats;

    public TextMeshProUGUI clipAmmoText;
    public TextMeshProUGUI reserveAmmoText;
    public TextMeshProUGUI ammoUsageText;

    public PlayerMovementController playerMovement;

    public int ammoBoxAmount;

    public int ammo;

    public Gun[] guns;
    private uint m_currentGun = 0;

    private void Start()
    {
        guns[m_currentGun].RefreshGUI(clipAmmoText, ammoUsageText, reserveAmmoText, ref ammo);
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            guns[m_currentGun].gameObject.SetActive(false);
            m_currentGun = (m_currentGun + 1) % (uint)guns.Length;
            guns[m_currentGun].gameObject.SetActive(true);
            guns[m_currentGun].RefreshGUI(clipAmmoText, ammoUsageText, reserveAmmoText, ref ammo);
        }

        if(Input.GetMouseButtonDown(0))
        {
            guns[m_currentGun].StartShooting(clipAmmoText);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            guns[m_currentGun].StopShooting();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            guns[m_currentGun].Reload(clipAmmoText, reserveAmmoText, ref ammo);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ammo"))
        {
            ammo = Math.Min(ammo + ammoBoxAmount, playerStats.maxAmmo);
            guns[m_currentGun].RefreshGUI(clipAmmoText, reserveAmmoText, ref ammo);
            Destroy(other.gameObject);
        }
    }
}
