using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerBlock : Powerup
{
    public SteerBlockData steerBlockData;

    public override void PowerupStart(PlayerController playerController)
    {
        if (playerController.powerups.ContainsKey(PowerupType.ULTIMATE))
        {
            playerController.powerups.Remove(PowerupType.STEERBLOCK);
            return;
        }
        if(GameManager.Instance.Lifelines > 1)
        {
            playerController.powerups.Remove(PowerupType.STEERBLOCK);
            GameManager.Instance.TakeLifeline(1);
            return;
        }

        playerController.stunned.gameObject.SetActive(true);
        playerController.allowSteering = false;
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.allowSteering = true;
        playerController.stunned.gameObject.SetActive(false);
    }

    public override void AddTime()
    {
        TimeActiveLeft += steerBlockData.timeActive;
    }
}
