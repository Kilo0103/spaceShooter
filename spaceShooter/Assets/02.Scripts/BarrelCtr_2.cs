using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtr_2 : MonoBehaviour
{
    private Rigidbody rb;
    //폭발 효과 프리팹을 저장할 변수
    public GameObject expEffect;
    private int hitCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision coll)
    {
        //충돌시 태그확인
        if (coll.collider.CompareTag("BULLET")) // coll.collider.tag ==
        {
            //총알의 충돌 횟수가 3발인지 체크
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        //폭발 효과 프리팹을 동적으로 생성
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);
        //Rigidbody 컴포넌트의 mass를 1.0으로 수정해 무게를 가볍게 함
        rb.mass = 1.0f;
        //위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * 1000.0f);
    }
}
