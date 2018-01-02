using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCasting : NetworkBehaviour {

    public CameraController cameraController;
    public Spells2[] spell;
    public KeyCode[] keys;
    private int castingIndex;
    private bool isCasting;

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer)
            return;

        if (!GameController.settingsOpen && !cameraController.isBallFocused) {
            if (!cameraController.isBallFocused) {
                for (int i = 0; i < keys.Length; i++) {
                    if (Input.GetKeyDown(keys[i])) {
                        if (spell[i].canCast()) {
                            if (spell[i].hologramPrefab != null) {
                                castingIndex = i;
                                isCasting = true;
                                spell[i].showHologram();
                            } else {
                                if (isCasting) {
                                    stopHologram();
                                }
                                spell[i].Cast();
                            }
                        }
                    }
                }
                if (isCasting) {
                    if (Input.GetMouseButtonDown(0)) {
                        Debug.Log(spell[castingIndex]);
                        spell[castingIndex].Cast();
                        stopHologram();

                    }
                     //if right click
                     else if (Input.GetMouseButtonDown(1)) {
                        stopHologram();
                    }

                     //showHologram
                     else {
                        spell[castingIndex].updateHologram();
                    }
                }
            }
        }




    }

    public void stopHologram() {
        spell[castingIndex].deleteHologram();
        castingIndex = -1;
        isCasting = false;
    }
}