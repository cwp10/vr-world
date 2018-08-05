using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // 레티클 초기 색상
    private Color originColor;
    // 조준했을 때의 레티클 색상
    public Color fireColor = Color.red;
    // 조준점 이미지를 연결할 변수
    private Image crossHair;
    // 조준점의 색상을 변경할 동안 비활성화할 Animator 컴포넌트를 연결할 변수
    private Animator crossHairAnim;

    // Weapon에 추가된 Animator 컴포넌트
    private Animator weaponAnim;
    // 총의 처음 위치를 저장하기 위한 변수
    private Vector3 originPosition;

	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
        camTr = Camera.main.GetComponent<Transform>();
        // CrossHair 추출후 변수에 저장
        crossHair = GameObject.Find("CrossHair").GetComponent<Image>();
        // CrossHair 게임오브젝트에 포함된 Animator 컴포넌트를 추출한 다음 변수에 저장
        crossHairAnim = crossHair.GetComponent<Animator>();
        // 레티클의 초기 색상을 저장
        originColor = crossHair.color;

        muzzleFlash.enabled = false;

        // Animator 컴포넌트 추출
        weaponAnim = GetComponent<Animator>();
        // 총의 처음 위치를 저장
        originPosition = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        // 레이케스트가 적 캐릭터를 관통 했는지 여부 확인
        if (Physics.Raycast(camTr.position, camTr.forward, out hit, 100.0f, 1 << LayerMask.NameToLayer("ENEMY"))) {
            isFire = true;
            // 색상을 변경하기 전에 Animator 컴포넌트 비활성화
            crossHairAnim.enabled = false;
            // 레티클의 색상 변경
            crossHair.color = fireColor;
        } else {
            isFire = false;
            // Animator 컴포넌트 활성화
            crossHairAnim.enabled = true;
            // 레티클의 최초 색상으로 환원
            crossHair.color = originColor;
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
        // 총의 롤링 에니메이션을 정지
        weaponAnim.enabled = false;
        // x, y 값의 난수 발생
        Vector2 _random = Random.insideUnitCircle;
        // 총을 앞뒤와 상하로 이동시킴
        this.transform.localPosition += new Vector3(0, _random.x * 0.01f, _random.y * 0.01f);

        muzzleFlash.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
        muzzleFlash.enabled = true;
        _audio.PlayOneShot(fireSfx);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        muzzleFlash.enabled = false;

        // 총의 원래위치로 환원
        this.transform.localPosition = originPosition;
        // 총의 롤링 애니메이션을 재가동
        weaponAnim.enabled = true;
    }
}
