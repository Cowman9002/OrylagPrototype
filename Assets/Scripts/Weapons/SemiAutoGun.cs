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

    public override void StartShooting(TextMeshProUGUI clipAmmoText)
    {
        if(Time.time > m_nextShootTime && m_ammo > 0)
        {
            m_nextShootTime = Time.time + delay;
            m_ammo -= 1;
            Fire();
            RefreshGUI(clipAmmoText);
        }
    }

    protected override void Fire()
    {
        base.Fire();

        GameObject logic = Instantiate(bullet, shootOrigin.position, shootOrigin.rotation);
        logic.GetComponent<Bullet>().SetGFXPosition(graphicsOrigin.position);
    }
}
