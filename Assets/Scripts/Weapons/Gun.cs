using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
    public Transform graphicsOrigin;
    public Transform shootOrigin;

    public int ammoUsage;

    public AudioClip shootSound;

    protected AudioSource audioSource;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void RefreshAmmoGui(TextMeshProUGUI ammoText, int ammo)
    {
        ammoText.text = string.Format("{0}", ammo);
        
    }

    public virtual void StartShooting(TextMeshProUGUI ammoText, ref int ammo) { Fire(); }

    public virtual void StopShooting() { }

    protected virtual void Fire() { }

    protected virtual void PlayShootSound()
    {
        audioSource.Stop();
        audioSource.clip = shootSound;
        audioSource.Play();
    }

}
