using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerY : MonoBehaviour
{
    public PlayerController playerController;

    private bool shouldContinue = false;
    private float speed = 0f;
    
    void Update()
    {
        shouldContinue = !GameManager.Instance.isAlive;

        float positionY = 0f;
        if (shouldContinue)
        {
            speed = playerController.GetCurrentSpeed();
            positionY = transform.position.y + speed * Time.deltaTime;
        }
        else
            positionY = playerController.transform.position.y;

        transform.position = new Vector3(
            transform.position.x,
            positionY,
            transform.position.z);
    }
}
