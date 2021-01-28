using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    public abstract void Action();
    public virtual void Action(PlayerController playerController)
    {
        Action();
    }
}
