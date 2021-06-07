using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyDamage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    //생명 게이지
    private float hp = 100.0f;
    //초기 생명 수치
    private float initHp = 100.0f;
    //피격 시 사용할 혈흔 효과
    private GameObject bloodEffect;

    public GameObject hpBarPrefab; // hp바를 생성하기 위한 프리팹 리퍼런스 연결
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0); // 머리 위 (y축)로 표시하기 위한 오프셋

    private Canvas uiCanvas;    // 부모가 될 Canvas
    private Image hpBarImage;   // 생명 값에 따라 fillAmount 속성을 변경할 Image 오브젝트

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();

        //적 머리 위에 표시할 Hp바 오브젝트 실제 생성
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);

        // fillAmount 속성을 변경할 Image 객체 획득
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        // 생명 게이지가 따라가야 할 대상 및 offset 값 설정
        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    void Start()
    {
        //혈흔 효과 프리팹을 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
        SetHpBar();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == bulletTag)
        {
            //혈흔 효과를 생성하는 함수 호출
            ShowBloodEffect(coll);
            //총알 삭제
            //Destroy(coll.gameObject);
            coll.gameObject.SetActive(false);
            //생명 게이지 차감
            hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            //차감된 게이지 수치를 실제 오브젝트에 적용
            hpBarImage.fillAmount = hp / initHp;

            if (hp <= 0.0f)
            {
                //적 캐릭터의 상태를 DIE로 변경
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                // 적 캐릭터 사망 이후 게이지를 투명처리
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                // 적 캐릭터 사망 수를 누적 및 저장하는 메소드 실행
                GameManager.isnt.IncreaseKillCount();
                // 킬 카운트의 중복 계산을 막기 위한 콜라이더 비활성화
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    //혈흔 효과를 생성하는 함수
    void ShowBloodEffect(Collision coll)
    {
        //총알이 충돌한 지점 산출
        Vector3 pos = coll.contacts[0].point;
        //총알의 충돌했을 때의 법선 벡터
        Vector3 _normal = coll.contacts[0].normal;
        //총알의 충돌 시 방향 벡터의 회전값 계산
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
        //혈흔 효과 생성
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
}
