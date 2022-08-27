using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float lifeTime = 1.5f;

    private float _aliveTime = 0;
    Transform _transform;

    public void OnReturn() => gameObject.SetActive(false);

    public void OnGet()
    {
        _aliveTime = 0;
        gameObject.SetActive(true);
    }

    private void Start()
    {
        _transform = transform;
    }
    private void Update()
    {
        _aliveTime += Time.deltaTime;
        _transform.position +=speed * Time.deltaTime * _transform.forward;
        if (_aliveTime > lifeTime)
            BulletPool.Instance.ReturnToPool(this);
    }
}
