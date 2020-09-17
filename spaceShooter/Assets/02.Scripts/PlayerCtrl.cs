using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    private float h = 0.0f;
    private float v = 0.0f;

    [SerializeField] private Transform tr;
    //이동 속도 변수 선언
    public float moveSpeed = 10.0f;
    //회전 변수
    public float rotSpeed = 80.0f;

    // Start is called before the first frame update
    void Start()
    {
        //tr 변수에 Transform컴포넌트 할당
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal"); //h (수평의) 이동 입력값을 불러옴
        v = Input.GetAxis("Vertical");   //V (수직의) 이동 입력값을 불러옴

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //tr.Translate(이동 방향 * 속도 * 전진 * 전후진 변수 * Time.deltaTime, 기준좌표계);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        Debug.Log("h =" + h.ToString()); //디버그 창에 h 값을 출력
        Debug.Log("v =" + v.ToString()); //디버그 창에 v 값을 출력

    }
}
