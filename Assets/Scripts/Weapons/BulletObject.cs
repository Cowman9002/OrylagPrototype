using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Weapons/BulletObject")]
public class BulletObject : ScriptableObject
{
    public GameObject gfx;

    public float lifeTime;
    public float speed;
}
