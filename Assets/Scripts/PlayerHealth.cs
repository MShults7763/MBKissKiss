using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health the player can have
    public int currentHealth; // Current health of the player

    void Start()
    {
        // Initialize the current health to the max health at the start
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Subtract damage from current health
        currentHealth -= damage;

        // Ensure current health doesn't drop below zero
        currentHealth = Mathf.Max(currentHealth, 0);

        // If health reaches zero, you can call a death function
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        // Add heal amount to current health
        currentHealth += healAmount;

        // Ensure current health doesn't exceed max health
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    void Die()
    {
        // Here you can handle the player's death, like playing an animation or restarting the game
        Debug.Log("Player has died!");
    }
}
