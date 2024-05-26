using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Ship _ship;

    void Start()
    {
        _ship = this.gameObject.GetComponent<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Controller();
    }

    void Controller()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (vertical > 0)
        {
            _ship.Thrust(vertical);
        } else if (vertical < 0)
        {
            _ship.Brake(Math.Abs(vertical));
        }

        if (horizontal != 0)
        {
            _ship.Rotate(-horizontal);
        }
    }
}
