using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float _timeOffset;
    [SerializeField] private Vector3 _posOffset;
    private Vector3 _velocity;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + _posOffset, ref _velocity, _timeOffset);
    }
}