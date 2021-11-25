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

    public int clipSize;
    public int ammoUsage;

    protected int m_ammo;

    public AudioClip shootSound;

    protected AudioSource audioSource;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void StartShooting(TextMeshProUGUI clipAmmoText) { Fire(); }

    public virtual void StopShooting() { }

    public virtual void Reload(TextMeshProUGUI clipAmmoText, TextMeshProUGUI reserveAmmoText, ref int ammo)
    {
        int reserveToClip = ammoUsage > 0 ? ammo / ammoUsage : clipSize;

        int ammoRemoved = Math.Min(reserveToClip, clipSize - m_ammo);
        m_ammo += ammoRemoved;
        ammo -= ammoRemoved * ammoUsage;

        RefreshGUI(clipAmmoText, reserveAmmoText, ref ammo);
    }

    public void RefreshGUI(TextMeshProUGUI clipAmmoText, TextMeshProUGUI ammoUsageText, TextMeshProUGUI reserveAmmoText, ref int ammo)
    {
        if(reserveAmmoText != null) reserveAmmoText.text = string.Format("/ {0}", ammo);
        if(ammoUsageText != null) ammoUsageText.text = string.Format("({0}:{1})", clipSize, ammoUsage);
        if(clipAmmoText != null) clipAmmoText.text = string.Format("{0}", m_ammo);
    }

    public void RefreshGUI(TextMeshProUGUI clipAmmoText, TextMeshProUGUI reserveAmmoText, ref int ammo)
    {
        if (reserveAmmoText != null) reserveAmmoText.text = string.Format("/ {0}", ammo);
        if (clipAmmoText != null) clipAmmoText.text = string.Format("{0}", m_ammo);
    }

    public void RefreshGUI(TextMeshProUGUI clipAmmoText)
    {
        if (clipAmmoText != null) clipAmmoText.text = string.Format("{0}", m_ammo);
    }

    protected virtual void Fire() { }

    protected virtual void PlayShootSound()
    {
        audioSource.Stop();
        audioSource.clip = shootSound;
        audioSource.Play();
    }

}
