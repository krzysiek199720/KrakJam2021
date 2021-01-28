using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerY : MonoBehaviour
{
    public Transform playerTransform;
    
    void Update()
    {
        float playerY = playerTransform.position.y;
        transform.position = new Vector3(
            transform.position.x,
            playerY,
            transform.position.z);
    }
}
