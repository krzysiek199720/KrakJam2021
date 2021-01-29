using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup
{    public override void PowerupStart(PlayerController playerController)
    {
        GameManager.Instance.AddLifeline(1);
    }

    public override void PowerupEnd(PlayerController playerController){}

    public override void AddTime() {}
}