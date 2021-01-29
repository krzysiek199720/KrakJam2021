using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : Powerup
{
    public UltimateData ultimateData;

    public override void PowerupStart(PlayerController playerController)
    {
        playerController.isUltimateActive = true;
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.isUltimateActive = false;
    }

    public override void AddTime()
    {
        TimeActiveLeft += ultimateData.timeActive;
    }

    public float GetPercentDone()
    {
        return 100 * (TimeActiveLeft / ultimateData.timeActive);
    }
}