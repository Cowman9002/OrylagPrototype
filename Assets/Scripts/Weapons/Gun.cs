using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public Transform graphicsOrigin;
    public Transform shootOrigin;

    public AudioClip shootSound;

    protected AudioSource audioSource;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void StartShooting() { Fire(); }

    public virtual void StopShooting() { }

    protected virtual void Fire() { }

    protected virtual void PlayShootSound()
    {
        audioSource.Stop();
        audioSource.clip = shootSound;
        audioSource.Play();
    }

}
