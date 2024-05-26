using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private Camera cam;
    [SerializeField] private CinemachineVirtualCamera vcam;
    private float minHeight = 40;
    private float maxHeight = 60;
    private Color color = new Color(0, 125f / 255f, 205f / 255f);
    
    void Update()
    {
        if (this.ship.transform.position.y > minHeight)
        {
            this.cam.backgroundColor = color - color * Math.Min((this.ship.transform.position.y - minHeight) / (maxHeight - minHeight), 1);
            this.vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 4 +  Math.Min((this.ship.transform.position.y - minHeight) / (maxHeight - minHeight) * 10, 10);
        }
    }
}
