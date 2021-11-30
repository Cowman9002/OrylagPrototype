using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public Health health;
    public Slider healthBar;

    public PlayerMovementController playerMovement;

    private void Start()
    {
        healthBar.maxValue = health.maxHealth;
        healthBar.value = health.CurrentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            int dmg = other.GetComponent<Bullet>().stats.damage;
            health.DecreaseHealth(dmg, other.transform.position, -other.transform.forward, "Enemy");
            healthBar.value -= dmg;

            playerMovement.PlaySound(playerMovement.playerStats.hurtSound);

            if (health.IsDead)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (other.CompareTag("HurtBox"))
        {
            int dmg = other.GetComponent<HurtBox>().GetDamage();
            health.DecreaseHealth(dmg, other.ClosestPoint(transform.position), -other.transform.forward, "Enemy");
            healthBar.value -= dmg;

            playerMovement.PlaySound(playerMovement.playerStats.hurtSound);

            other.gameObject.SetActive(false);

            if (health.IsDead)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
