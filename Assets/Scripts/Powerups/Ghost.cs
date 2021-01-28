using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Powerup
{
    public GhostData ghostData;

    public override void PowerupStart(PlayerController playerController)
    {
        playerController.isGhost = true;
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.isGhost = false;
    }

    public override void AddTime()
    {
        TimeActiveLeft += ghostData.timeActive;
    }
}