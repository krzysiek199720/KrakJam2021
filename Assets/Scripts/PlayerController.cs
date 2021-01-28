using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerControllerData data;

    private Vector2 desiredMovement = Vector2.zero;
    public float speedModifier = 1f;
    public float basicSpeedMultiplier = 1f;
    public bool allowSteering = true;

    private Rigidbody2D rb2d;

    public Dictionary<PowerupType, Powerup> powerups;

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
        DoBasicMultiplier();

        //MOVEMENT
        // smooth axis for now, can change to GetAxisRaw
        desiredMovement = new Vector2(Input.GetAxis("Horizontal") * data.speed, data.climbSpeed * speedModifier * basicSpeedMultiplier);

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

    private void DoBasicMultiplier()
    {
        basicSpeedMultiplier = GameManager.Instance.CalculateSpeedMultiplier();
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
        // Tutaj w switch przydalo by sie ogarniac priorytet powerupow
        // np jak masz slowa to sie nie da speedup -> powerupy sie anuluja?

        switch (type)
        {
            case PowerupType.SPEEDUP:
            {
                    if (powerups.ContainsKey(PowerupType.SLOWDOWN))
                    {
                        powerups.Remove(PowerupType.SLOWDOWN);
                    }
                    Speedup powerup = new Speedup();
                    SpeedupData sd = Resources.Load<SpeedupData>("ScriptableData/SpeedupData");
                    powerup.speedupData = sd;
                    powerup.AddTime();
                    powerups.Add(PowerupType.SPEEDUP, powerup);
                    thePowerup = powerup;
                    break;
            }
            case PowerupType.SLOWDOWN:
            {
                    if (powerups.ContainsKey(PowerupType.SPEEDUP))
                    {
                        powerups.Remove(PowerupType.SPEEDUP);
                    }
                    SlowDown powerup = new SlowDown();
                    SlowDownData sd = Resources.Load<SlowDownData>("ScriptableData/SlowdownData");
                    powerup.slowDownData = sd;
                    powerup.AddTime();
                    powerups.Add(PowerupType.SLOWDOWN, powerup);
                    thePowerup = powerup;
                    break;
            }
            default:
                Debug.LogError("Unknown PowerupType");
                return;
        }
        thePowerup.PowerupStart(this);
        GameManager.Instance.AddPowerupScore();
    }
}
