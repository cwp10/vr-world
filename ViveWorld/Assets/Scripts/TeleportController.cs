using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController :MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_TrackedController trackedCtrl;

    private SteamVR_Controller.Device controller {
        get {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private LineRenderer laser;

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        trackedCtrl = GetComponent<SteamVR_TrackedController>();

        laser = GetComponent<LineRenderer>();
        laser.enabled = false;
    }

    private void OnEnable() {
        trackedCtrl.PadTouched += TrackPad;
        trackedCtrl.PadUntouched += ReleaseTrackPad;
    }

    private void OnDisable() {
        trackedCtrl.PadTouched -= TrackPad;
        trackedCtrl.PadUntouched -= ReleaseTrackPad;
    }

    void TrackPad(object sender, ClickedEventArgs e) {
        // 진동 발생
        controller.TriggerHapticPulse(2000);
        Debug.Log("Pad is touched");

        // 레이저를 활성화
        laser.enabled = true;
    }

    void ReleaseTrackPad(object sender, ClickedEventArgs e) {
        // 레이저를 비활성화
        laser.enabled = false;
    }
}
