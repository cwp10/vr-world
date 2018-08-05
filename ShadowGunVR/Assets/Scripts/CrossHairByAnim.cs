using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairByAnim : MonoBehaviour {

    private Animator anim;
    private int hashIsGaze;

    public bool isGaze
    {
        set 
        {
            anim.SetBool(hashIsGaze, value);
        }
    }
	void Start () {
        anim = GetComponent<Animator>();
        // 에니메이터 뷰에 선언된 isGaze 파라미터의 해시값 추출 및 저장
        hashIsGaze = Animator.StringToHash("isGaze");
	}
}
