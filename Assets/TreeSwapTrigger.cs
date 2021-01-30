using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSwapTrigger : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        TreeRepeat tree = collision.gameObject.GetComponentInParent<TreeRepeat>();
        if (tree)
        {
            tree.SwapRepeat();
        }
    }
}
