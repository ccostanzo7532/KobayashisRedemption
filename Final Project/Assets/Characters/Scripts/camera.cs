using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Camera : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera vcam2D;

    public void Awake()
    {
        player = Player.Instance.transform;
        vcam2D = this.GetComponent<CinemachineVirtualCamera>();
    }


    // Update is called once per frame
    void Update()
    {

        vcam2D.m_Follow = player;
    }
}
