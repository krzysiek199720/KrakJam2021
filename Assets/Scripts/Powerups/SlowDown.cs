using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : Powerup
{
    public SlowDownData slowDownData;

    public override void PowerupStart(PlayerController playerController)
    {
        if (playerController.powerups.ContainsKey(PowerupType.ULTIMATE))
        {
            playerController.powerups.Remove(PowerupType.SLOWDOWN);
            return;
        }
        playerController.speedModifier = slowDownData.speedModifier;
        playerController.slower.gameObject.SetActive(true);
        AudioController.Instance.Play(SoundId.Collect_slow);
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.speedModifier = 1f;
        playerController.slower.gameObject.SetActive(false);
    }

    public override void AddTime()
    {
        TimeActiveLeft += slowDownData.timeActive;
    }
}
