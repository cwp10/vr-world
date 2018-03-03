using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveController : MonoBehaviour {
  
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device controller {
        get {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	

	void Update () {
        if (controller == null) {
            Debug.Log("Controller is not detected");
            return;
        }

        // 트리거  버튼 클릭
        if (controller.GetHairTriggerDown()) {
            Debug.Log("Trigger " + controller.index + " is pressed");
        }

        // 트리거 버튼 릴리즈
        if (controller.GetHairTriggerUp()) {
            Debug.Log("Trigger " + controller.index + " is unpressed");
        }

        // 메뉴 버튼 클릭
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
            Debug.Log(controller.index + " App Button pressed");
        }

        // 메뉴 버튼 릴리즈
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            Debug.Log(controller.index + " App Button unpressed");
        }

        // 트랙패드 터치 여부와 좌표 산출
        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) {
            Vector2 pad = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
            Debug.Log("TouchPad = " + controller.index + " " + pad);
        }
    }
}
