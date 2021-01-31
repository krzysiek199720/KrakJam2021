using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Powerup
{
    public GhostData ghostData;

    public override void PowerupStart(PlayerController playerController)
    {
        playerController.isGhost = true;

        playerController.animator.SetBool("isGhost", true);
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.isGhost = false;

        playerController.animator.SetBool("isGhost", false);
    }

    public override void AddTime()
    {
        TimeActiveLeft += ghostData.timeActive;
    }
}