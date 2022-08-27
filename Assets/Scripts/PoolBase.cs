using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject
{
    public void OnReturn();
    public void OnGet();
}
public abstract class PoolBase<T> : MonoBehaviour where T : Component,IPoolObject
{
    [SerializeField] private T prefab;
    [Tooltip("Start the pool with a reasonable amount of objects that should be avalible")]
    [SerializeField] private int startCount=0;

    public static PoolBase<T> Instance { get; private set; }

    protected Queue<T> _objects = new Queue<T>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        AddToPool(startCount);
    }


    public virtual void ReturnToPool(T returnObject)
    {
        returnObject.OnReturn();
        _objects.Enqueue(returnObject);
    }

    public virtual T Get()
    {
        if (_objects.Count < 1)
        {
            AddToPool(1);
        }
        var obj = _objects.Dequeue();
        obj.OnGet();
        return obj;
    }

    protected virtual void AddToPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            _objects.Enqueue(newObject);
        }
    }
}
