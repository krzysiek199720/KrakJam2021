using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Powerup
{
    public GhostData ghostData;

    public override void PowerupStart(PlayerController playerController)
    {
        playerController.isGhost = true;
        
        Color tmp = playerController.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.5f;
        playerController.GetComponent<SpriteRenderer>().color = tmp;
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.isGhost = false;

        Color tmp = playerController.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        playerController.GetComponent<SpriteRenderer>().color = tmp;
    }

    public override void AddTime()
    {
        TimeActiveLeft += ghostData.timeActive;
    }
}