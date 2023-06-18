using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 playerPos;
    [SerializeField] private float camHeight;   // Default: 15f
    [SerializeField] private float camOffset;   // Default: 5f

    void LateUpdate()
    {
        playerPos = player.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + camHeight, playerPos.z - camOffset);
        transform.LookAt(playerPos);
    }
}
