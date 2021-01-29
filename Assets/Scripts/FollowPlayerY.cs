using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerY : MonoBehaviour
{
    public PlayerController playerController;

    private float localShift = 0f;

    private bool shouldContinue = false;
    private float speed = 0f;

    private void Start()
    {
        localShift = playerController.transform.position.y;
    }

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
            positionY - localShift,
            transform.position.z);
    }
}
