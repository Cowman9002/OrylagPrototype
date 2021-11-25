using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public BulletObject stats;
    private Rigidbody body;

    public void SetGFXPosition(Vector3 pos)
    {
        Transform gfx = transform.GetChild(0);

        if (gfx)
        {
            gfx.position = pos;
        }
    }

    void Start()
    {
        Destroy(gameObject, stats.lifeTime);

        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * stats.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
