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

    void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update() {
        if (controller == null) {
            Debug.Log("Controller is not detected");
            return;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "BALL") {
            gripedObj = other.transform;

            var _renderer = gripedObj.GetComponent<Renderer>();
            originColor = _renderer.material.color;
            _renderer.material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "BALL") {
            gripedObj.GetComponent<Renderer>().material.color = originColor;
            gripedObj = null;
        }
    }
}
