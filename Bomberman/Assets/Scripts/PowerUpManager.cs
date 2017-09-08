using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    List<powerup> CollectedPowerUps = new List<powerup>();
    List<powerdown> CollectedSkulls = new List<powerdown>();

    PlayerMove player;

    // Use this for initialization
    void Start () {
        player = gameObject.GetComponent<PlayerMove>();
	}

    public void AddPowerUp(powerup power) {
        CollectedPowerUps.Add(power);
        UpdatePlayerEffects();
    }

    public void AddPowerDown(powerup skull)
    {
        CollectedPowerUps.Add(skull);
        UpdatePlayerEffects();
    }

    void UpdatePlayerEffects () {
        int blastRange = player.blastRange;
        int bombCapacity = player.bombCapacity;
        float speed = 5;   player.speed = speed;
        bool remoteDetonate = player.remoteDetonate;
        bool kick =  player.kick;
        bool punch = player.punch;

        foreach (powerup power in CollectedPowerUps) {
            blastRange += power.rangeBoost;
            bombCapacity += power.carryBoost;
            speed += power.speedBoost;
            if (power.remoteDetonate)
                remoteDetonate = true;
            if (power.kick)
                kick = true;
            if (power.punch)
                kick = true;
        }

		player.blastRange = blastRange;
        player.bombCapacity = bombCapacity;
        player.speed = speed;
        player.remoteDetonate = remoteDetonate;
        player.kick = kick;
        player.punch = punch;

	}
}
[System.Serializable]
public struct powerup {
    public int rangeBoost;
    public int carryBoost;
    public int speedBoost;
    public bool remoteDetonate;
    public bool kick;
    public bool punch;
}

public struct powerdown
{
    bool tooFast;
    bool tooSlow;
    bool bombSickness;
    bool noBombs;
    bool invisible;
    bool constanBombs;
    bool rangeReduce;
}