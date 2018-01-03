using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Wall : Spells2 {

    public float range;
    public Vector3 offset;
    private GameObject hologram;
	
    public override void cast() {
        Debug.Log("wall-isServer: " + isServer);

        if (!isLocalPlayer) 
            return;

        currentCharges--;
		Ray ray = player.cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit;
		
        if (Physics.Raycast(ray, out hit, range)) {
            Vector3 loc = hit.point + offset;
			Quaternion rotation = Quaternion.Euler(-90, player.transform.eulerAngles.y, -90);
			CmdWall(loc, rotation);
		} else if (hologram != null) {
			CmdWall(hologram.transform.position, hologram.transform.rotation);
			
        }
    }

	[Command]
	public void CmdWall( Vector3 loc, Quaternion rotation) {
		anim.Play("Wall");
		GameObject obj = Instantiate(effect, loc, rotation);
		NetworkServer.Spawn(obj);
	}

	public override void showHologram() {
        if(hologram == null) {
            Ray ray = player.cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range)) {
                Vector3 loc = hit.point + offset;
                hologram = Instantiate(hologramPrefab, loc, Quaternion.Euler(-90, player.transform.eulerAngles.y, -90));
            }
        } else {
            deleteHologram();
        }
    }

    public override void deleteHologram() {
        Destroy(hologram);
        hologram = null;
    }

    public override void updateHologram() {
        if (hologram != null) {
            Ray ray = player.cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range)) {
                Vector3 loc = hit.point + offset;
                hologram.transform.position = loc;
                hologram.transform.rotation = Quaternion.Euler(-90, player.transform.eulerAngles.y, -90);
            }
        }
    }

	
}
