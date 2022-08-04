using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health=50.0f;

    public void ReduceDamage(float amount)
    {
        Debug.Log("Helath reduced");
        health-= amount;
        if (health < 0.001f)
            Destroy(gameObject);
    }
}
