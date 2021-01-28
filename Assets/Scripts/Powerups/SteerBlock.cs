using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerBlock : Powerup
{
    public SteerBlockData steerBlockData;

    public override void PowerupStart(PlayerController playerController)
    {
        playerController.allowSteering = false;
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.allowSteering = true;
    }

    public override void AddTime()
    {
        TimeActiveLeft += steerBlockData.timeActive;
    }
}
