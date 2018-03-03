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

    private RaycastHit hit;
    private int floorLayer;
    private Transform tr;
    private Transform playerTr;

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        trackedCtrl = GetComponent<SteamVR_TrackedController>();

        laser = GetComponent<LineRenderer>();
        laser.enabled = false;

        // 컨트롤러의 transform 컴포넌트 추출
        tr = GetComponent<Transform>();
        // CameraRig 게임오브젝트의 transform 컴포넌트 추출
        playerTr = GameObject.Find("[CameraRig]").GetComponent<Transform>();
        // 바닥의 레이어를 미리 계산한 후 변수에 저장
        floorLayer = 1 << LayerMask.NameToLayer("FLOOR");
    }

    private void OnEnable() {
        trackedCtrl.PadTouched += TrackPad;
        trackedCtrl.PadUntouched += ReleaseTrackPad;
        trackedCtrl.PadClicked += RayCastAtLaser;
    }

    private void OnDisable() {
        trackedCtrl.PadTouched -= TrackPad;
        trackedCtrl.PadUntouched -= ReleaseTrackPad;
        trackedCtrl.PadClicked -= RayCastAtLaser;
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

    void RayCastAtLaser(object sender, ClickedEventArgs e) {
        if (laser.enabled) {
            // 레이케스트로 이동할 위치 산출
            if (Physics.Raycast(tr.position, tr.forward, out hit, Mathf.Infinity, floorLayer)) {
                MovePlayer(hit.point);
            }
        }
    }

    // 이동 로직 처리
    void MovePlayer(Vector3 pos) {
        // 현재 높이값은 그대로 적용하기 위해 이동할 좌표 수정
        Vector3 movePos = new Vector3(pos.x, playerTr.position.y, pos.z);
        playerTr.position = movePos;
    }
}
