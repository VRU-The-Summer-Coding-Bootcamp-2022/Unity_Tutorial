using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour,IPoolObject
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float degreeRange = 90f;
    [SerializeField] private float smoothness = 0.1f;
    [SerializeField] private float changeDirectionTime = 1.5f;
    [SerializeField] private float maxLifeTime = 5f;

    private float _lifeTime = 0;
    private float _changeDirTime = 0;
    private Transform _transform;
    private Quaternion _newDirection;

    public void OnGet() 
    {
        gameObject.SetActive(true);
        _lifeTime = 0;
    }

    public void OnReturn() => gameObject.SetActive(false);

    private void Start() => _transform = transform;
    private void Update()
    {
        _lifeTime+=Time.deltaTime;
        if (_lifeTime > maxLifeTime)
            AntPool.Instance.ReturnToPool(this);

        _changeDirTime+=Time.deltaTime;
        if (_changeDirTime >= Random.Range(changeDirectionTime/2, changeDirectionTime))
        {

            _changeDirTime = 0;
            _newDirection=Quaternion.Euler(0, Random.Range(-degreeRange, degreeRange), 0);
        }

        _transform.position += speed * Time.deltaTime * _transform.forward;
        _transform.rotation = Quaternion.Lerp(_transform.rotation, _newDirection, smoothness);
    }
}
