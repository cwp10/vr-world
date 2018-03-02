using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour {

    private AudioSource _audio;
    public AudioClip fireSfx;

    // 총구 화염 메쉬
    public MeshRenderer muzzleFlash;
    // 총알의 궤적을 표현하는 메쉬의 Transform 컴포넌트
    public Transform bulletTrail;
    // 발사 간격
    public float fireRate = 0.08f;
    // 다음번 발사 시각을 저장할 변수 
    private float nextFire = 0.0f;

    // 발사여부를 판별할 변수
    public bool isFire = false;

    private RaycastHit hit;
    private Transform camTr;

	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
        camTr = Camera.main.GetComponent<Transform>();
        muzzleFlash.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        // 레이케스트가 적 캐릭터를 관통 했는지 여부 확인
        if (Physics.Raycast(camTr.position, camTr.forward, out hit, 100.0f, 1 << LayerMask.NameToLayer("ENEMY"))) {
            isFire = true;
        } else {
            isFire = false;
        }

        if (isFire) {
            if (Time.time >= nextFire) {
                nextFire = Time.time + fireRate;
                // 적 케릭터에 Hit 함수를 호출
                hit.collider.SendMessage("Hit", SendMessageOptions.DontRequireReceiver);
                // 총구 화염 효과
                StartCoroutine(FireEffect());
            }
            // 총알의 궤적을 표현하기 위해 z축 스케일을 변경
            float z = Random.Range(1.0f, 6.0f);
            bulletTrail.localScale = new Vector3(bulletTrail.localScale.x, bulletTrail.localScale.y, z);
        } else {
            bulletTrail.localScale = new Vector3(bulletTrail.localScale.x, bulletTrail.localScale.y, 0.0f);
        }
	}

    // 총구화염효과
    IEnumerator FireEffect() {
        muzzleFlash.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
        muzzleFlash.enabled = true;
        _audio.PlayOneShot(fireSfx);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        muzzleFlash.enabled = false;
    }
}
