using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float range = 5;
    [SerializeField] private float damage;
    [SerializeField] Transform shootingPoint;
    [SerializeField] LayerMask mask;
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;

    private float _fireTime=0;
    private void Update()
    {
        _fireTime += Time.deltaTime;
        bool canShoot = _fireTime > (1 / fireRate);

        if (canShoot && Input.GetMouseButton(0))
        {
            //var bulletObj = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
            var bulletObj = BulletPool.Instance.Get();
            bulletObj.gameObject.SetActive(true);
            bulletObj.transform.position = shootingPoint.position;
            bulletObj.transform.rotation = shootingPoint.rotation;
            _fireTime = 0;
            if(Physics.Raycast(shootingPoint.position, shootingPoint.forward, out var hitinfo, range, mask))
            {
                var damages = new List<DamageType>();
                damages.Add(new NormalDamage(5));
                hitinfo.transform.GetComponent<Health>()?.ReduceHealth(damages);
            }
        }
    }

}
