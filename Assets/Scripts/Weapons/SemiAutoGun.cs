using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SemiAutoGun : Gun
{
    public GameObject bullet;

    public float delay;

    private float m_nextShootTime = 0;

    public override void StartShooting(TextMeshProUGUI ammoText, ref int ammo)
    {
        if(Time.time > m_nextShootTime && ammo >= ammoUsage)
        {
            PlayShootSound();

            m_nextShootTime = Time.time + delay;
            ammo -= ammoUsage;
            Fire();
            RefreshAmmoGui(ammoText, ammo);
        }
    }

    protected override void Fire()
    {
        base.Fire();
        GameObject logic = Instantiate(bullet, shootOrigin.position, shootOrigin.rotation);
        logic.GetComponent<Bullet>().SetGFXPosition(graphicsOrigin.position);
    }
}
