﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup
{    public override void PowerupStart(PlayerController playerController)
    {
        GameManager.Instance.AddLifeline(1);
        playerController.shield.SetActive(true);
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.shield.SetActive(false);
    }

    public override void AddTime() {}
}