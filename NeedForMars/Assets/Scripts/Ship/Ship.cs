using System;using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _speed = 10f;
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _maxRotationSpeed = 10f;
    [SerializeField] private float _brakeSpeed = 10f;
    [SerializeField] private float _maxBrakeSpeed = 10f;

    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private GameObject _leftEngine;
    private bool _engineDetached = false;

    [SerializeField] private ParticleSystem[] particleSystems;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        
        if (_rb == null)
        {
            throw new Exception("Rigidbody2D not found on Ship");
        }
    }

    void Update()
    {
        if (this.transform.position.y > 80 && !_engineDetached)
        {
            this._rightEngine.transform.SetParent(null);
            this._leftEngine.transform.SetParent(null);

            this._rightEngine.GetComponent<Rigidbody2D>().simulated = true;
            this._rightEngine.GetComponent<Rigidbody2D>().velocity = this._rb.velocity;
            this._rightEngine.GetComponent<Rigidbody2D>().AddForce(Vector2.right, ForceMode2D.Impulse);
            this._rightEngine.GetComponent<Rigidbody2D>().AddTorque(-1, ForceMode2D.Impulse);
            this._leftEngine.GetComponent<Rigidbody2D>().simulated = true;
            this._leftEngine.GetComponent<Rigidbody2D>().velocity = this._rb.velocity;
            this._leftEngine.GetComponent<Rigidbody2D>().AddForce(Vector2.left, ForceMode2D.Impulse);
            this._leftEngine.GetComponent<Rigidbody2D>().AddTorque(1, ForceMode2D.Impulse);
            
            _engineDetached = true;
        }
    }

    public void Thrust(float speed)
    {
        if (_rb.velocity.magnitude < _maxSpeed)
        {
            _rb.AddForce(this.transform.up * _speed * Mathf.Clamp(speed, 0, _maxSpeed));
        }
        
        foreach(ParticleSystem ps in particleSystems)
        {
            ps.Emit(1 );
        }
    }

    public void Brake(float speed)
    {
        if (_rb.velocity.magnitude > 0)
        {
            _rb.AddForce(-this.transform.up * Mathf.Clamp(_brakeSpeed * speed, 0, _maxBrakeSpeed));
        }
    }

    public void Rotate(float degree)
    {
        float currentAngularVelocity = _rb.angularVelocity;
        
        if ((degree > 0 && currentAngularVelocity < _maxRotationSpeed) || (degree < 0 && currentAngularVelocity > -_maxRotationSpeed))
        {
            _rb.AddTorque(Mathf.Clamp(_rotationSpeed * degree, -_maxRotationSpeed, _maxRotationSpeed));
        }
    }
}
