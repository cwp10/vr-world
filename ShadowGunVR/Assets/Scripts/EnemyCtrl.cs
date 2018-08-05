using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour {

    // Animator 컴포넌트를 할당하기 위한 변수
    private Animator anim;
    // Animator Controller의 hit 파라미터에 접근하기 위한 헤쉬값
    private int idHit;
    // Animator Controller의 die 파라미터에 접근하기 위한 헤쉬값
    private int idDie;
    // 적 케릭터 생명치
    private int hp = 100;

    // Use this for initialization
    void Start () {
        // Animator 컴포넌트 할당
        anim = GetComponent<Animator>();
        // Animator Controller에 선언된 파라미터의 해시값을 추출한 후 변수에 저장
        idHit = Animator.StringToHash("Hit");
        idDie = Animator.StringToHash("Die");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // 피격됐을 때 호출될 함수
    void Hit() {
        hp -= 10;
        // hit 트리거 발생
        anim.SetTrigger(idHit);

        if (hp <= 0) {
            // 레이케스트를 Default 로 변경
            this.gameObject.layer = 0;
            // Capsule collider 비활성화
            GetComponent<CapsuleCollider>().enabled = false;

            // Die 트리거 발생
            anim.SetTrigger(idDie);
            // 5초 후 삭제
            Destroy(this.gameObject, 5.0f);
        }
    }
}
