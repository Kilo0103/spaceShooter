using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runF;
    public AnimationClip runB;
    public AnimationClip runL;
    public AnimationClip runR;
}

public class PlayCtrl_2 : MonoBehaviour
{

    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;

    [SerializeField] private Transform tr;
    //이동 속도 변수 선언
    public float moveSpeed = 10.0f;
    //회전 변수
    public float rotSpeed = 80.0f;

    // 인스펙터 뷰에 표시할 애니메이션 클래스 변수
    public PlayerAnim playerAnim;

    //Animation 컴포넌트를 저장하기 위한 변수
    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        //tr 변수에 Transform컴포넌트 할당
        tr = GetComponent<Transform>();

        //Animation 컴포넌트를 변수에 할당
        anim = GetComponent<Animation>();
        //Animation 컴포넌트의 애니메이션 클립을 지정하고 실행
        anim.clip = playerAnim.idle;
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal"); //h (수평의) 이동 입력값을 불러옴
        v = Input.GetAxis("Vertical");   //V (수직의) 이동 입력값을 불러옴
        r = Input.GetAxis("Mouse X");    //r (마우스) 의 값을 불러옴


        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //tr.Translate(이동 방향 * 속도 * 전진 * 전후진 변수 * Time.deltaTime, 기준좌표계);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        //tr.Rotate(Y축을 기준으로 * 회전 속도 * Time.deltaTime * 마우스);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        Debug.Log("h =" + h.ToString()); //디버그 창에 h 값을 출력
        Debug.Log("v =" + v.ToString()); //디버그 창에 v 값을 출력
        Debug.Log("r =" + r.ToString()); //디버그 창에 v 값을 출력

        if (v >= 0.0001f) //민감하게 변경했습니다.
        {
            anim.CrossFade(playerAnim.runF.name, 0.3f); //전진
        }
        else if (v <= -0.0001f)
        {
            anim.CrossFade(playerAnim.runB.name, 0.3f); //후진
        }
        else if (h >= 0.0001f)
        {
            anim.CrossFade(playerAnim.runR.name, 0.3f); //오른쪽
        }
        else if (h <= -0.0001f)
        {
            anim.CrossFade(playerAnim.runL.name, 0.3f); //왼쪽
        }
        else
        {
            anim.CrossFade(playerAnim.idle.name, 0.3f); //대기
        }


    }
}
