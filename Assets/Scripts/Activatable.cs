using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    public virtual void Action()
    {
        Debug.LogError("Method not implemented");
    }
    public virtual void Action(PlayerController playerController)
    {
        Action();
    }
}
