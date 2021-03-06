﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerY : MonoBehaviour
{
    public PlayerController playerController;

    private float localShift = 0f;

    private bool shouldContinue = false;
    private float speed = 0f;

    [HideInInspector]
    public bool isActive = true;

    private void Start()
    {
        localShift = playerController.transform.position.y;
    }

    void Update()
    {
        if (!isActive)
            return;

        shouldContinue = !GameManager.Instance.isAlive;

        float positionY = 0f;
        if (shouldContinue)
        {
            speed = playerController.GetCurrentSpeed();
            positionY = transform.position.y + speed * Time.deltaTime;
        }
        else
            positionY = playerController.transform.position.y - localShift;

        transform.position = new Vector3(
            transform.position.x,
            positionY,
            transform.position.z);
    }
}
