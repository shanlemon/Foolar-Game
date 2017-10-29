using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

	public Transform player;
	public int cooldown, charges;
	public int currentCD = 0, currentCharges = 1;

	public enum Types {
		projectile,
		aoe,
		movement
	}
	public Types type;
	public GameObject effect;

	void FixedUpdate () {
		if (currentCD > 0) {
			currentCD--;

			if (currentCD <= 0) {
				if (charges > 1 && currentCharges < charges) {
					currentCharges++;
					currentCD = cooldown;
				}
			}
		}
	}

	public void cast (){
		if (currentCD <= 0 || charges > 1) {
			switch (type) {
			case 0:
				if (charges == 1) {
					fireProjectile ();
					currentCD = cooldown;
				} else {
					if (currentCharges > 0) {
						fireProjectile ();
						if (currentCharges == charges)
							currentCD = cooldown;
						currentCharges--;
					}
				}
				break;
			}
		}
	}

	void fireProjectile () {
		Instantiate (effect, player.position, player.rotation);
	}

}
