using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Spells2 {

    public float dashStrength;
    public float dashTime;

    public override void CmdCast(InputSent input) {
        currentCharges--;
        StartCoroutine(dash());
    }

    IEnumerator dash() {

        Physics.gravity = Vector3.zero;
        player.rb.velocity = player.cam.transform.forward * dashStrength;

        yield return new WaitForSeconds(dashTime);

        Physics.gravity = new Vector3(0, PlayerController.normalGrav, 0);
        


        

    }

    public override void deleteHologram() {
        throw new NotImplementedException();
    }

    public override void showHologram() {
        throw new NotImplementedException();
    }

    public override void updateHologram() {
        throw new NotImplementedException();
    }

}
