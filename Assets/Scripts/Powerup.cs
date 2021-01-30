using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    public float TimeActiveLeft = 0f;
    public abstract void PowerupStart(PlayerController playerController);
    public abstract void PowerupEnd(PlayerController playerController);

    public abstract void AddTime();

    // Returns true if powerup is ending
    public bool TickTime(float tick)
    {
        TimeActiveLeft -= tick;
        return TimeActiveLeft <= 0f;
    }
}
