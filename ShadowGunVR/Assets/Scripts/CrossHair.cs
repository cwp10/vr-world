using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour {

    private Transform tr;
    // 조준점 이미지를 연결할 변수
    private Image crossHair;
    // 레티클이 커지기 시작하는 시각을 저장할 변수 
    private float startTime;
    // 레티클이 커지는 속도
    public float duration = 0.2f;
    // 레티클 최소 크기
    public float minSize = 0.4f;
    // 레티클 최대 크기
    public float maxSize = 0.6f;

    // 레티클의 초기 색상
    private Color originColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    // 응시했을 때의 레티클 색상
    public Color gazeColor = Color.green;

    // 응시여부를 결정하는 변수
    public static bool isGaze = false;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        crossHair = GetComponent<Image>();
        startTime = Time.time;

        // 레티클의 초기 크기를 설정
        tr.localScale = Vector3.one * minSize;
        // 레티클의 초기 색상를 저장
        crossHair.color = originColor;
	}
	
	// Update is called once per frame
	void Update () {
        if (isGaze)
        {
            // 증가 시간 계산
            float t = (Time.time - startTime) / duration;

            // 레티클의 크기를 점진적으로 증가 시킴
            tr.localScale = Vector3.one * Mathf.Lerp(minSize, maxSize, t);
            // 레티클의 색상 변경
            crossHair.color = gazeColor;
        }
        else
        {
            // 레티클을 최초 크기와 색상으로 환원
            tr.localScale = Vector3.one * minSize;
            crossHair.color = originColor;

            // 시간 초기화
            startTime = Time.time;
        }
	}
}
