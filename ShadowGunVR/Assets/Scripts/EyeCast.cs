using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EyeCast : MonoBehaviour {
    private Transform tr;
    private Ray ray;
    private RaycastHit hit;
    private CrossHairByAnim CrossHair;

    public float dist = 10.0f;

    private GameObject prevButton; // 이전에 응시했던 버튼을 저장할 변수
    private GameObject currButton; // 현재 응시하고 있는 버튼을 저장할 변수

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        CrossHair = GameObject.Find("CrossHair").GetComponent<CrossHairByAnim>();
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

        // 버튼의 응시 여부 표시
        CheckGazeButton();
	}

    void CheckGazeButton() {
        // 포인터 이벤트 정보 추출
        PointerEventData data = new PointerEventData(EventSystem.current);

        // 9번 레이어 검출
        if (Physics.Raycast(ray, out hit, dist, 1 << 9))
        {
            // ray에 맞는 버튼을 현재 버튼으로 설정
            currButton = hit.collider.gameObject;

            // 이전 버튼과 현재 버튼이 서로 다를 경우
            if (currButton != prevButton)
            {
                // 현재 버튼에 PointerEnter 이벤트 전달
                ExecuteEvents.Execute(currButton, data, ExecuteEvents.pointerEnterHandler);
                // 이전 버튼에 PointerExit 이벤트 전달
                ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
                // 이전 버튼 정보를 갱신
                prevButton = currButton;
            }
            else
            {
                // 버튼을 벗어나 다른 곳을 응시했을 때 기존 버튼에 PointExit 이벤트 전달
                if (prevButton != null)
                {
                    ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
                    prevButton = null;
                }
            }
        }
    }
}
