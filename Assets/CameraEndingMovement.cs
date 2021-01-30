using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraEndingMovement : MonoBehaviour
{
    public SpriteRenderer treeTop;
    public float distanceToDarken = 0.8f;
    public Rigidbody2D rb2d;

    public float timeToPan = 2f;

    private bool isRunning = false;

    private float startVal = 0f;
    private float timeSum = 0f;

    private PlayerController playerController;

    public void StartEndingSequence()
    {
        FollowPlayerY followPlayerY = GetComponent<FollowPlayerY>();
        startVal = rb2d.position.y;
        playerController = followPlayerY.playerController;

        followPlayerY.isActive = false;

        isRunning = true;
    }

    public void Update()
    {
        if (!isRunning)
            return;

        timeSum += Time.deltaTime;
        playerController.speedModifier -= Time.deltaTime;
        float newYPos = Mathf.Lerp(startVal, treeTop.transform.position.y + 6f, Mathf.Clamp01(timeSum / timeToPan));
        
        rb2d.MovePosition(new Vector2(rb2d.position.x, newYPos));

        float result = treeTop.transform.position.y + 4f - transform.position.y;

        if(result < distanceToDarken)
        {
            GameManager.Instance.FadeOut();
        }

        if (Mathf.Abs(result) < 0.25f)
        {
            GameManager.Instance.EndGame();
            isRunning = false;
        }

    }
}
