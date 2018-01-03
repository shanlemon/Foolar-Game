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
	public Animator anim;
	public float delay;

	void Start() {
		anim = player.anim;
	}

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

	//[Command]
	//public abstract void CmdCast(GameObject effect, Vector3 loc, Quaternion rotation);

	public abstract void cast();


	public abstract void showHologram();
    public abstract void deleteHologram();
    public abstract void updateHologram();

}