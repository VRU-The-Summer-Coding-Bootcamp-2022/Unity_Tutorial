using UnityEngine;

public class BadSpawn : MonoBehaviour
{
    [SerializeField]private float fireRate;
    [SerializeField] Vector3 offcet = Vector3.zero;
    
    private Transform _transform;
    private float _fireTime;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _fireTime += Time.deltaTime;
        bool canShoot = _fireTime > (1 / fireRate);
        if (canShoot)
        {
            _fireTime = 0;
            var ant = AntPool.Instance.Get();
            ant.transform.position = _transform.position + offcet;
            ant.transform.rotation= _transform.rotation;
        }
    }
}
