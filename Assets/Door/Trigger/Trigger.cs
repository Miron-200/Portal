using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Door _Door;
    [SerializeField] private bool isClose;
    [HideInInspector]  public MeshRenderer MeshLight;
    [HideInInspector] public Color[] ColorIdicator;

    private void Awake()
    {
        MeshLight.materials[0].color = ColorIdicator[isClose ? 0 : 1];
        _Door.SetNumeKey();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_Door.StatusDoor() != isClose)
        {
            _Door.LockDoor(isClose);          
        }
    }
}
