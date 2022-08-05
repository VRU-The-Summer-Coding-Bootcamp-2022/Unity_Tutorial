using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float health=100;
    public UnityEvent onZeroHealth = new UnityEvent();
    public void ReduceDamage(float amount)
    {
        Debug.Log("Helath reduced");
        health -= amount;
        if (health < 0.001f)
        {
            if (onZeroHealth == null) Debug.Log("null");
            onZeroHealth.Invoke(); 
        }
    }
}
