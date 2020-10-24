using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet_2 : MonoBehaviour
{
    //스파크 프리팹을 저장할 변수
    public GameObject sparkEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            //스파크 효과 함수 호출
            ShowEffect(coll);
            //충돌한 게임오브젝트 삭제
            Destroy(coll.gameObject,1.0f);
        }
    }
    void ShowEffect(Collision coll)
    {
        //충돌 지점의 정보를 추출
        ContactPoint contact = coll.contacts[0];
        //법선 벡터가 이루는 회전각도 추출
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);

        //스파크 효과를 생성
        GameObject spark = Instantiate(sparkEffect, contact.point, rot);
        //스파크 효과의 부모를 드럼통 또는 벽으로 설정
        spark.transform.SetParent(this.transform);
    }
}