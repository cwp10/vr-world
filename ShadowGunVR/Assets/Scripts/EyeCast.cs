using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EyeCast : MonoBehaviour {
    private Transform tr;
    private Ray ray;
    private RaycastHit hit;
    private CrossHairByAnim CrossHair;

    public float dist = 10.0f;

    private GameObject prevButton; // 이전에 응시했던 버튼을 저장할 변수
    private GameObject currButton; // 현재 응시하고 있는 버튼을 저장할 변수

    public Image circleBar; // 응시한 버튼의 하위에 있는 circleBar 이미지를 wkjwkdgkf qustn
    public float selectTime = 1.0f; // 프로그래스바가 채워지는 지속 시간
    public float passTime = 0.0f;   // 응시한 후 지난 시간
    private bool isClicked = false; // 버튼의 클릭 여부

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
        //포인터 이벤트 정보 추출
        PointerEventData data = new PointerEventData(EventSystem.current);

        //9번 레이어(버튼)를 검출
        if (Physics.Raycast(ray, out hit, dist, 1 << 9)) {
            //Ray에 맞은 버튼을 현재 버튼으로 설정
            currButton = hit.collider.gameObject;
            //응시한 버튼의 하위에 있는 CircleBar의 Image컴포넌트 추출
            circleBar = currButton.GetComponentsInChildren<Image>()[1];

            //이전 버튼과 현재 버튼이 서로 다를 경우
            if (currButton != prevButton) {
                //응시시간 초기화
                passTime = 0.0f;
                //Image 컴포넌트의 fillAmount 속성의 초기화
                circleBar.fillAmount = 0.0f;
                //버튼 클릭의 초기화
                isClicked = false;

                if (prevButton != null) {
                    //이전에 응시한 버튼이 있을 경우 fillAmount 값을 초기화
                    prevButton.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
                }

                //현재 버튼은 OnPointerEnter 이벤트를 전달
                ExecuteEvents.Execute(currButton, data, ExecuteEvents.pointerEnterHandler);
                //이전 버튼은 OnPointerExit 이벤트를 전달
                ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
                //이전 버튼의 정보를 갱신
                prevButton = currButton;
            } else if (isClicked != true) //이전 버튼과 현재 버튼이 동일하고 클릭되지 않았을 경우
              {
                //응시 지속시간을 계속 누적
                passTime += Time.deltaTime;
                //Image 컴포넌트의 fillAmount 속성값을 증가
                circleBar.fillAmount = passTime / selectTime;

                //응시 지속시간이 설정값보다 클 경우 클릭으로 판단
                if (passTime >= selectTime) {
                    //Debug.Log(currButton.name + " is clicked!");
                    //현재 버튼에 OnPointerClick 이벤트를 전달
                    ExecuteEvents.Execute(currButton, data, ExecuteEvents.pointerClickHandler);
                    //클릭이 중복해서 발생하지 않기위해 변수설정
                    isClicked = true;
                }
            }
        } else {   //버튼을 벋어나 다른 곳을 응시했을때 기존 버튼에 OnPointerExit 이벤트 전달
            if (prevButton != null) {
                ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
                prevButton.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
                prevButton = null;
                passTime = 0.0f;
            }
        }
    }
}
