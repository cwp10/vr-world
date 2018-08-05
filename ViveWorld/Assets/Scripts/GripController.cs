using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripController : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device controller {
        get {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private Transform gripedObj;
    private Color originColor;

    private bool isGripped = false;

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update() {
        if (controller == null) {
            Debug.Log("Controller is not detected");
            return;
        }

        // 트리거 버튼을 클릭했을 경우
        if (controller.GetHairTriggerDown() && gripedObj != null) {
            // 컨트롤러로 잡은 게임오브젝트를 차일드화
            gripedObj.SetParent(this.transform);
            // 컨트롤러에 차일드화 된 후 물리 시뮬레이션 연산을 하지 않음
            gripedObj.GetComponent<Rigidbody>().isKinematic = true;

            // 컨트롤러가 잡은 객체가 있을으로 설정
            isGripped = true;
        }

        // 트리거 버튼을 릴리즈 했을 경우
        if (controller.GetHairTriggerUp() && gripedObj != null) {
            var rb = gripedObj.GetComponent<Rigidbody>();
            if (rb != null) {
                // 물리 시뮬레이션의 연산 기능을 다시 활성화
                rb.isKinematic = false;
                // 현재 컨트롤러의 속도와 회전역을 공에 전달
                rb.velocity = controller.velocity;
                rb.angularVelocity = controller.angularVelocity;
            }
            // 차일드화된 공의 부모를 루트로 변경
            gripedObj.SetParent(null);
        }
    }

    private void OnTriggerEnter(Collider other) {
        // 접촉한 객체가 BALL 태그이고 잡은 객체가 없을 경우
        if (other.tag == "BALL" && !isGripped) {
            // 컨트롤러가 잡은 객체 정보를 저장
            gripedObj = other.transform;

            var _renderer = gripedObj.GetComponent<Renderer>();
            // 공의 초기 색상을 저장
            originColor = _renderer.material.color;
            // 컨트롤러가 접속한 공의 색상을 빨간색으로 변경
            _renderer.material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "BALL" && gripedObj.gameObject == other.gameObject) {
            // 릴리즈한 공의 색상을 초기 색상으로 지정
            gripedObj.GetComponent<Renderer>().material.color = originColor;
            // 컨트롤러가 잡은 객체 정보를 초기화
            gripedObj = null;
            // 잡은 객체가 없음으로 설정
            isGripped = false;
        }
    }
}
