using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    GameObject player;
    public float speed = 5;
    public string targetTag = "char_ctrl";

    void FixedUpdate()
    {
        player = GameObject.Find(targetTag);
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        }
    }
}