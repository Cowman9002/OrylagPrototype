using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject hitParticles;

    public int maxHealth;
    public int CurrentHealth { get; private set; }

    public string LastInjuryCause { get; private set; }

    public bool IsDead { get; private set; }

    private void Start()
    {
        CurrentHealth = maxHealth;
        LastInjuryCause = null;
    }

    public void ClearLastInjury()
    {
        LastInjuryCause = null;
    }

    public void DecreaseHealth(int amount, Vector3 hitPos, Vector3 hitDir, string cause)
    {
        if (IsDead) return;

        LastInjuryCause = cause;
        CurrentHealth -= amount;

        if(hitParticles)
        {
            GameObject go = GameObject.Instantiate(hitParticles, hitPos, Quaternion.LookRotation(hitDir));
            GameObject.Destroy(go, go.GetComponent<ParticleSystem>().main.duration);
        }

        if (CurrentHealth <= 0) IsDead = true;
    }

    public void IncreaseHealth(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
    }
}
