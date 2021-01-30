using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerControllerData data;
    public Animator animator;

    private Vector2 desiredMovement = Vector2.zero;
    public float speedModifier = 1f;
    public float basicSpeedMultiplier = 1f;
    public bool allowSteering = true;
    public bool isGhost = false;
    public bool isUltimateActive = false;

    private Rigidbody2D rb2d;
    public bool shouldGameRun = false;

    public Dictionary<PowerupType, Powerup> powerups;
    public Dictionary<PowerupType, BasicPowerupData> powerupsData;

    public GameObject shield;
    public GameObject bees_swarm;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
            Debug.LogError("No Rigidbody2D found");
        powerups = new Dictionary<PowerupType, Powerup>();
        powerupsData = new Dictionary<PowerupType, BasicPowerupData>();
    }

    void FixedUpdate()
    {
        if (!shouldGameRun)
            return;
        if(!GameManager.Instance.isAlive)
        {
            // smierc
            shouldGameRun = false;
            return;
        }

        DoPowerups();
        DoBasicMultiplier();

        animator.SetFloat("walkSpeed", GetCurrentSpeedModifier());

        //MOVEMENT
        // smooth axis for now, can change to GetAxisRaw
        desiredMovement = new Vector2(
            allowSteering ? Input.GetAxis("Horizontal") * data.speed : 0f,
            GetCurrentSpeed());

        Vector3 newPosition = rb2d.position + desiredMovement * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, data.positionMinX, data.positionMaxX);

        rb2d.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Activatable activatable = collision.GetComponent<Activatable>();
        if (activatable)
        {
            activatable.Action(this);
        }
    }

    public float GetCurrentSpeed()
    {
        return data.climbSpeed * GetCurrentSpeedModifier();
    }

    public float GetCurrentSpeedModifier()
    {
        return speedModifier * basicSpeedMultiplier;
    }

    private void DoBasicMultiplier()
    {
        basicSpeedMultiplier = GameManager.Instance.CalculateSpeedMultiplier();
    }

    private void DoPowerups()
    {
        List<PowerupType> powerupsToDelete = new List<PowerupType>();
        foreach (var powerup in powerups)
        {
            bool result = powerup.Value.TickTime(Time.fixedDeltaTime);
            if (result)
            {
                powerupsToDelete.Add(powerup.Key);
                powerup.Value.PowerupEnd(this);
            }
        }

        foreach (var item in powerupsToDelete)
        {
            powerups.Remove(item);
        }
    }

    public void AddPowerup(PowerupType type)
    {
        if (powerups.ContainsKey(type) && type != PowerupType.ULTIMATE)
        {
            Debug.Log(type);
            powerups[type].AddTime();
            return;
        }
        
        // usuwanie steerblock powerupa
        if(type != PowerupType.STEERBLOCK && powerups.ContainsKey(PowerupType.STEERBLOCK))
        {
            SteerBlock sb = (SteerBlock) powerups[PowerupType.STEERBLOCK];
            sb.PowerupEnd(this);
            powerups.Remove(PowerupType.STEERBLOCK);
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
                    SpeedupData sd;
                    if (powerupsData.ContainsKey(PowerupType.SPEEDUP))
                        sd = (SpeedupData) powerupsData[PowerupType.SPEEDUP];
                    else
                    {
                        sd = Resources.Load<SpeedupData>("ScriptableData/SpeedupData");
                        powerupsData.Add(PowerupType.SPEEDUP, sd);
                    }
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
                    SlowDownData sd;
                    if (powerupsData.ContainsKey(PowerupType.SLOWDOWN))
                        sd = (SlowDownData)powerupsData[PowerupType.SLOWDOWN];
                    else
                    {
                        sd = Resources.Load<SlowDownData>("ScriptableData/SlowdownData");
                        powerupsData.Add(PowerupType.SLOWDOWN, sd);
                    }
                    powerup.slowDownData = sd;
                    powerup.AddTime();
                    powerups.Add(PowerupType.SLOWDOWN, powerup);
                    thePowerup = powerup;
                    break;
            }
            case PowerupType.STEERBLOCK:
            {
                    SteerBlock powerup = new SteerBlock();
                    SteerBlockData sd;
                    if (powerupsData.ContainsKey(PowerupType.STEERBLOCK))
                        sd = (SteerBlockData)powerupsData[PowerupType.STEERBLOCK];
                    else
                    {
                        sd = Resources.Load<SteerBlockData>("ScriptableData/SteerBlockData");
                        powerupsData.Add(PowerupType.STEERBLOCK, sd);
                    }
                    powerup.steerBlockData = sd;
                    powerup.AddTime();
                    powerups.Add(PowerupType.STEERBLOCK, powerup);
                    thePowerup = powerup;
                    break;
            }
            case PowerupType.GHOST:
            {
                    Ghost powerup = new Ghost();
                    GhostData gd;
                    if (powerupsData.ContainsKey(PowerupType.GHOST))
                        gd = (GhostData)powerupsData[PowerupType.GHOST];
                    else
                    {
                        gd = Resources.Load<GhostData>("ScriptableData/GhostData");
                        powerupsData.Add(PowerupType.GHOST, gd);
                    }
                    powerup.ghostData = gd;
                    powerup.AddTime();
                    powerups.Add(PowerupType.GHOST, powerup);
                    thePowerup = powerup;
                    break;
            }
            case PowerupType.ULTIMATE:
            {
                    powerups.Remove(PowerupType.ULTIMATE);
                    Ultimate powerup = new Ultimate();
                    UltimateData ud;
                    if (powerupsData.ContainsKey(PowerupType.ULTIMATE))
                        ud = (UltimateData)powerupsData[PowerupType.ULTIMATE];
                    else
                    {
                        ud = Resources.Load<UltimateData>("ScriptableData/UltimateData");
                        powerupsData.Add(PowerupType.ULTIMATE, ud);
                    }
                    powerup.ultimateData = ud;
                    powerup.AddTime();
                    powerups.Add(PowerupType.ULTIMATE, powerup);
                    thePowerup = powerup;
                    break;
            }
            case PowerupType.SHIELD:
                {
                    thePowerup = new Shield();
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
