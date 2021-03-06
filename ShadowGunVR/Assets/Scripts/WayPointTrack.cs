﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointTrack : MonoBehaviour {

    public Color lineColor = Color.yellow;
    private Transform[] points;

    private void OnDrawGizmos()
    {
        // 라인의 색상 지정
        Gizmos.color = lineColor;
        // Waypoint 게임오브젝트 아래에 있는 모든 point 게임오브젝트 추출
        points = GetComponentsInChildren<Transform>();

        int nextIdx = 1;

        Vector3 currPos = points[nextIdx].position;
        Vector3 nextPos;

        // point 게임오브젝트를 순회 하면서 라인을 그림
        for (int i = 0; i <= points.Length; i++) {
            // 마지막 point 일 경우 첫번째 point로 지정
            nextPos = (++nextIdx >= points.Length) ? points[1].position : points[nextIdx].position;
            // 시작 위치에서 종료 위치까지 라인을 그림
            Gizmos.DrawLine(currPos, nextPos);
            currPos = nextPos;
        }
    }
}
