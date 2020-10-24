using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //총알의 파괴력
    public float damage = 20.0f;
    //총알 발사 속도
    public float speed = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody>().AddForce(transform.forward * speed); //글로벌 좌표 기준 앞으로
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed); // 로컬 기준 앞으로

        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}