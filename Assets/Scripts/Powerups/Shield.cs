using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup
{    public override void PowerupStart(PlayerController playerController)
    {
        GameManager.Instance.AddLifeline(1);
        playerController.shield.SetActive(true);

        AudioController.Instance.Play(SoundId.Collect_shield_activate);
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        
    }

    public override void AddTime() {}
}