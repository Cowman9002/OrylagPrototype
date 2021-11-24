using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoGun : Gun
{
    public GameObject bullet;

    public float delay;

    private float m_nextShootTime = 0;

    public override void StartShooting()
    {
        if(Time.time > m_nextShootTime)
        {
            m_nextShootTime = Time.time + delay;
            Fire();
        }
    }

    protected override void Fire()
    {
        base.Fire();

        GameObject logic = Instantiate(bullet, shootOrigin.position, shootOrigin.rotation);
        logic.GetComponent<Bullet>().SetGFXPosition(graphicsOrigin.position);
    }
}
