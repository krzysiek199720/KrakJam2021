using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerControllerData data;

    private Vector2 desiredMovement = Vector2.zero;
    public float speedModifier = 1f;
    public bool allowSteering = true;

    private Rigidbody2D rb2d;

    private Dictionary<PowerupType, Powerup> powerups;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
            Debug.LogError("No Rigidbody2D found");
        powerups = new Dictionary<PowerupType, Powerup>();
    }

    void FixedUpdate()
    {
        DoPowerups();

        //MOVEMENT
        // smooth axis for now, can change to GetAxisRaw
        desiredMovement = new Vector2(Input.GetAxis("Horizontal") * data.speed, data.climbSpeed * speedModifier);

        rb2d.MovePosition(rb2d.position + desiredMovement * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Activatable activatable = collision.GetComponent<Activatable>();
        if (activatable)
        {
            activatable.Action(this);
        }
    }

    private void DoPowerups()
    {
        List<PowerupType> powerupsToDelete = new List<PowerupType>();
        foreach (var powerup in powerups.Values)
        {
            bool result = powerup.TickTime(Time.fixedDeltaTime);
            if (result)
            {
                powerupsToDelete.Add(powerup.type);
                powerup.PowerupEnd(this);
            }
            Debug.Log(powerup.TimeActiveLeft);
        }

        foreach (var item in powerupsToDelete)
        {
            powerups.Remove(item);
        }
    }

    public void AddPowerup(PowerupType type)
    {
        if (powerups.ContainsKey(type))
        {
            powerups[type].AddTime();
            return;
        }
        Powerup thePowerup = null;
        switch (type)
        {
            case PowerupType.SPEEDUP:
            {
                    Speedup powerup = new Speedup();
                    SpeedupData sd = Resources.Load<SpeedupData>("ScriptableData/SpeedupData");
                    powerup.speedupData = sd;
                    powerup.AddTime();
                    powerups.Add(PowerupType.SPEEDUP, powerup);
                    thePowerup = powerup;
                    break;
            }
            default:
                Debug.LogError("Unknown PowerupType");
                return;
        }
        thePowerup.PowerupStart(this);
    }
}
