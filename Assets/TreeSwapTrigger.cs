using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSwapTrigger : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        IRepeat repeat = collision.gameObject.GetComponentInParent<IRepeat>();
        if (repeat != null)
        {
            repeat.SwapRepeat();
        }
    }
}
