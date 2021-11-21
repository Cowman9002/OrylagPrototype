using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletOrigin;
    public Transform shootOrigin;

    public void Shoot()
    {
        Bullet b = Instantiate(bullet, shootOrigin.position, shootOrigin.rotation).GetComponent<Bullet>();
        b.Initialize(bulletOrigin);
    }
}
