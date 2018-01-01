using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Spells2 : NetworkBehaviour {

    
    public enum InputSent {
        rightClick,
        leftClick,
        keyboard
    }
    public GameObject hologramPrefab;
    public GameObject effect;

    private void FixedUpdate() {
        if (currentCharges < charges) {
            currentCooldown++;
            if (currentCooldown >= cooldown) {
                currentCharges++;
                currentCooldown = 0;
            }
        }
    }

    public bool canCast() {
        return currentCharges > 0;
    }

    public PlayerController player;
    public int cooldown, charges, currentCooldown, currentCharges;

    [Command]
    public abstract void CmdCast(InputSent input);

    public abstract void showHologram();
    public abstract void deleteHologram();
    public abstract void updateHologram();



}
