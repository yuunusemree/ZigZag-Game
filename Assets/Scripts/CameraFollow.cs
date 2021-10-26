using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 diffVector;
    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        diffVector = player.position - transform.position;
    }
    void LateUpdate()
    {
        transform.position = player.position - diffVector;    
    }
    
}
