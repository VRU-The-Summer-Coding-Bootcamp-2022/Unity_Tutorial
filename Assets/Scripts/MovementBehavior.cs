using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class MovementBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float xinput;
    private float yinput;

    private Rigidbody _rb;
    private Camera _cam;
    private Transform _transform ; 
    private Transform _camTransform;
    private Vector3 _debug=Vector3.zero;
    private void Start()
    {
        _transform = transform;
        _cam = Camera.main;
        _camTransform = _cam.transform;

        _rb = GetComponent<Rigidbody>();
        print(_rb);
    }
    private void Update()
    {
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");

        LookAtMouse();
    }
    private void LookAtMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos += _camTransform.forward * _cam.nearClipPlane;
        var direction = _cam.ScreenToWorldPoint(mousePos) - _camTransform.position;
        Ray ray = new Ray(_camTransform.position, direction);
        var _p = new Plane(Vector3.up, _transform.position);
        if (_p.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            _debug = hitPoint;
            _transform.LookAt(hitPoint);
        }
    }

    private void FixedUpdate()
    {
        var new_velocity = Vector3.ClampMagnitude(new Vector3(xinput, 0, yinput) * speed, speed);
        new_velocity = new Vector3(new_velocity.x, _rb.velocity.y, new_velocity.z);
        _rb.velocity = new_velocity;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(_debug, 0.1f);
    //}
}
