using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform graphicsOrigin;
    public Transform shootOrigin;

    public virtual void StartShooting() { Fire(); }

    public virtual void StopShooting() { }

    protected virtual void Fire() { }

}
