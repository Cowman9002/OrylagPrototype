using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : SemiAutoGun
{
    public uint numBullets;
    public float spreadHalfAngle;

    protected override void Fire()
    {
        PlayShootSound();

        for (uint i = 0; i < numBullets; i++)
        {
            float anglex = Random.Range(-spreadHalfAngle, spreadHalfAngle);
            float angly = Random.Range(-spreadHalfAngle, spreadHalfAngle);

            GameObject logic = Instantiate(bullet, shootOrigin.position, shootOrigin.rotation);

            logic.transform.rotation = Quaternion.AngleAxis(anglex, logic.transform.up) * logic.transform.rotation;
            logic.transform.rotation = Quaternion.AngleAxis(angly, logic.transform.right) * logic.transform.rotation;


            logic.GetComponent<Bullet>().SetGFXPosition(graphicsOrigin.position);
        }
    }
}
