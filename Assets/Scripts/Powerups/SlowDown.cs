using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : Powerup
{
    public SlowDownData slowDownData;

    public override void PowerupStart(PlayerController playerController)
    {
        playerController.speedModifier = slowDownData.speedModifier;
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.speedModifier = 1f;
    }

    public override void AddTime()
    {
        TimeActiveLeft += slowDownData.timeActive;
    }
}
