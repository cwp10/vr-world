using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_TrackedController trackedCtrl;

    private SteamVR_Controller.Device controller {
        get {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        trackedCtrl = GetComponent<SteamVR_TrackedController>();
    }

    private void OnEnable() {
        trackedCtrl.PadTouched += TrackPad;
    }

    private void OnDisable() {
        trackedCtrl.PadTouched -= TrackPad;
    }

    void TrackPad(object sender, ClickedEventArgs e) {
        // 진동 발생
        controller.TriggerHapticPulse(2000);
        Debug.Log("Pad is touched");
    }
}
