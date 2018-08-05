using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonResponder : MonoBehaviour {

    // EventTrigger 컴포넌트 변수
    private EventTrigger _trigger;

	// Use this for initialization
	void Start () {
        // 이벤트 트리거 컴포넌트 생성
        _trigger = this.gameObject.AddComponent<EventTrigger>();

        // 포인터엔터 이벤트 정의
        // 이벤트의 종류와 호출할 함수의 정보를 저장할 Entry 생성
        EventTrigger.Entry entry1 = new EventTrigger.Entry();

        // 마우스 커서 또는 레이케스트가 Hover 됐을때 발생하는 이벤트
        entry1.eventID = EventTriggerType.PointerEnter;

        // 이벤트가 발생했을 때 호출할 함수를 정의
        entry1.callback.AddListener(delegate {
            OnButtonHover(true);
        });

        // 이벤트 트리거에 포인트 엔터 이벤트 추가
        _trigger.triggers.Add(entry1);

        // 포인터엑시트 이벤트 정의
        // 이벤트의 종류와 호출할 함수의 정보를 저장할 Entry 생성
        EventTrigger.Entry entry2 = new EventTrigger.Entry();

        // 마우스 커서 또는 레이케스트가 Hover 됐을때 발생하는 이벤트
        entry2.eventID = EventTriggerType.PointerExit;

        // 이벤트가 발생했을 때 호출할 함수를 정의
        entry2.callback.AddListener(delegate {
            OnButtonHover(false);
        });

        // 이벤트 트리거에 포인트 엑시트 이벤트 추가
        _trigger.triggers.Add(entry2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnButtonHover(bool isHover) {
        Debug.Log("Button is hovered = " + isHover);
    }
}
