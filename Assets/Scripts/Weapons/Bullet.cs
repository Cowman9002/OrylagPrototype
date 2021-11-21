using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public BulletObject stats;
    private Rigidbody body;

    GameObject gfx;

    public void Initialize(Transform gfxOrigin)
    {
        gfx = Instantiate(stats.gfx, gfxOrigin.position, gfxOrigin.rotation);
        gfx.transform.SetParent(transform);
    }

    void Start()
    {
        Destroy(gameObject, stats.lifeTime);
        Destroy(gfx, stats.lifeTime);

        body = GetComponent<Rigidbody>();

        body.velocity = transform.forward * stats.speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Destroy(gfx);
    }
}
