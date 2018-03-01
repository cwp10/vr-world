using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCast : MonoBehaviour {
    private Transform tr;
    private Ray ray;
    private RaycastHit hit;

    public float dist = 10.0f;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();	
	}
	
	// Update is called once per frame
	void Update () {
        // 광선을 생성
        ray = new Ray(tr.position, tr.forward * dist);

        // 광선을 씬뷰에서 시각적으로 표시
        Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);

        // 8번과 9번 레이어만 검출하는 raycast
        if (Physics.Raycast(ray, out hit, dist, 1 << 8 | 1 << 9))
        {
            MoveCtrl.isStopped = true;
            CrossHair.isGaze = true;
        }
        else
        {
            MoveCtrl.isStopped = false;
            CrossHair.isGaze = false;
        }
	}
}
