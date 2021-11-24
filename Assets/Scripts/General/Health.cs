using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
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

    public void DecreaseHealth(int amount, string cause)
    {
        if (IsDead) return;

        LastInjuryCause = cause;
        CurrentHealth -= amount;

        if (CurrentHealth <= 0) IsDead = true;
    }

    public void IncreaseHealth(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
    }
}
