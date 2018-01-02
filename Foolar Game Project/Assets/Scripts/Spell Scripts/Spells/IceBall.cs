using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class IceBall : Spells2 {

    public override void Cast() {
        currentCharges--;
        Quaternion rotation = player.cam.transform.rotation;
        Instantiate(effect, player.transform.Find("Shoot Target").position, rotation);
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
