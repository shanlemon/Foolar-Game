using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spells2 : MonoBehaviour {

    
    public enum InputSent {
        rightClick,
        leftClick,
        keyboard
    }
    public GameObject hologramPrefab;
    public GameObject effect;

    private void Update() {
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

    public abstract void cast(InputSent input);
    public abstract void showHologram();
    public abstract void deleteHologram();
    public abstract void updateHologram();



}
