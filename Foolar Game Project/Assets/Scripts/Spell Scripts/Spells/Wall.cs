using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Spells2 {

    public float range;
    public Vector3 offset;
    private GameObject hologram;

    public override void cast(InputSent input) {
        currentCharges--;
		Ray ray = player.cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range)) {
            Vector3 loc = hit.point + offset;
            Instantiate(effect, loc, Quaternion.Euler(-90, player.transform.eulerAngles.y, -90));
        } else if (hologram != null) {
            Instantiate(effect, hologram.transform.position, hologram.transform.rotation);
        }   
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
