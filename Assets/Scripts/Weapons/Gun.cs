using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform bulletOrigin;

    public void Shoot()
    {
        GameObject bu = Instantiate(bulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
        Destroy(bu, 10.0f);

        bu.GetComponent<Rigidbody>().velocity = bu.transform.forward * bulletSpeed;
    }
}
