using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool> { };


public class PlayerNetworkScript : NetworkBehaviour {

    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;

    void DisablePlayer() {

    }

    void EnablePlayer() {
        onToggleShared.Invoke(true);

        if (isLocalPlayer) {
            onToggleLocal.Invoke(true);
        } else {
            onToggleRemote.Invoke(true);
        }

    }

}
