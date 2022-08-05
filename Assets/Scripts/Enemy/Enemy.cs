using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyScore=5;
    
    public void Kill()
    {
        Singelton.Instance.AddScore(enemyScore);
        Destroy(gameObject);
    }
}
