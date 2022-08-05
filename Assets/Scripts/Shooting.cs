using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float range = 5;
    [SerializeField] private float damage;
    [SerializeField] Transform shootingPoint;
    [SerializeField] LayerMask mask;
    private void Update()
    {
        Debug.DrawLine(shootingPoint.position, shootingPoint.position + shootingPoint.forward * range);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out var hitinfo, range, mask))
            {
                Debug.Log(hitinfo.transform.name);
                hitinfo.transform.GetComponent<Health>()?.ReduceDamage(damage);
            }
        }
    }

}
