using UnityEngine;
using System.Collections;
using System.Security.AccessControl;

public class FollowCam_5 : MonoBehaviour {
    public Transform targetTr;      //추적할 타겟 게임오브젝트의 Transform 변수
    public float dist = 2.4f;      //카메라와의 일정 거리
    public float height = 0.6f;     //카메라의 높이 설정
    public float dampTrace = 55.0f; //부드러운 추적을 위한 변수
    
    //카메라 자신의 Transform 변수
    private Transform tr;
    
    void Start () {
        //카메라 자신의 Transform 컴포넌트를 tr에 할당
        tr = GetComponent<Transform>();    	
    }
    
    //Update 함수 호출 이후 한번씩 호출되는 함수인 LateUpdate 사용
    //추적할 타겟의 이동이 종료된 이후에 카메라가 추적하기 위해 LateUpdate 사용
    void LateUpdate () {

        //카메라의 위치를 추적대상의 dist 변수만큼 뒤쪽으로 배치하고 
        //height 변수만큼 위로 올림

        //tr.postition = Vector3.Lerp(카메라 위치, 캐릭터의 위치 - dist + height, Time.deltaTime * dampTrace); <<< 시간의 변화에 따른 위치를 예측하는 Vector3.Lerp
        tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
        //카메라가 타겟 게임오브젝트를 바라보게 설정
        tr.LookAt(targetTr.position);
    }
}