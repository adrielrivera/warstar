using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightsaber : MonoBehaviour
{
    
    public int damage = 25;
    private Collider lightsaberCollider;

    void Start()
    {
        // Get the Collider component on the lightsaber
        lightsaberCollider = GetComponent<Collider>();
        // Disable the collider by default
        lightsaberCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called");
        // Check if the collider belongs to an enemy body
        if (other.CompareTag("EnemyBody"))
        {
            Debug.Log("Enemy hit");
            // Get the enemy script from the parent GameObject
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    // Method to enable the collider
    public void EnableCollider()
    {
        lightsaberCollider.enabled = true;
    }

    // Method to disable the collider
    public void DisableCollider()
    {
        lightsaberCollider.enabled = false;
    }
}
