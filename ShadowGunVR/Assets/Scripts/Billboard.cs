using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private Transform camTr;
    private Transform tr;

	void Start () {
        camTr = Camera.main.transform;
        tr = transform;
	}
	
	void LateUpdate () {
        // 메뉴를 메인카메라를 응시
        tr.LookAt(camTr.position);
	}
}
