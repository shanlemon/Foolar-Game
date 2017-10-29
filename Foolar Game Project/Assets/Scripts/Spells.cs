using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

	public Transform player;
	public int cooldown, charges;
	public int currentCD = 0, currentCharges = 1;
    public GameObject hologram;
    public bool isCasting = false;
    public float range = 10f;
    public Vector3 offset;

	public enum Types {
		projectile,
		aoe,
		movement,
        placeable
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

    void showHologram() {
        Vector3 holoPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        GameObject holoGram = Instantiate(hologram, holoPos, Quaternion.Euler(0,0,0));
        holoGram.transform.position = holoPos;
    }

    public void cast() {
        if (currentCD <= 0 || charges > 1) {
            if (charges == 1) {
                castType();
                currentCD = cooldown;
            } else {
                if (currentCharges > 0) {
                    castType();
                    if (currentCharges == charges)
                        currentCD = cooldown;
                    currentCharges--;
                }
            }
        }

    }

    void castType() {
        switch (type) {
            case Types.projectile:
                fireProjectile();
                break;
            case Types.placeable:
                placeProjectile();
                break;
        }
    }

	void fireProjectile () {
		Instantiate (effect, player.position, Camera.main.transform.rotation);
	}

    void placeProjectile() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f,.5f,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range)) {
            Vector3 loc = hit.point + offset;
            Instantiate(effect, loc, player.rotation);
        }

        

    }

}
