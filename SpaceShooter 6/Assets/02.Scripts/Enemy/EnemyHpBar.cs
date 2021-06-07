using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private Camera uiCamera;            // 게이지를 표시할 때 기준이 될 카레마 인스턴스의 리퍼런스
    private Canvas canvas;              // UI용 최 상위 캔버스

    private RectTransform rectParent;   // 부모 위치관련
    private RectTransform rectHp;       // HP게이지 위치 관련

    [HideInInspector]   //[SerializeField]
    public Vector3 offset = Vector3.zero; // HP바 이미지의 위치를 조정할 때 사용
    [HideInInspector]
    public Transform targetTr; //추적 대상의 Transform
    

    // Start is called before the first frame update
    void Start()
    {
        // 필요 컴포넌트 리퍼런스 획득 및 할당
        canvas = GetComponentInParent<Canvas>();    // 이 스크립트가 적용된 오브젝트가 올라가 있는
                                                    // 부모 오브젝트에서 Canvas 컴포넌트 획득
        uiCamera = canvas.worldCamera;      // Screen Space - Camera일 때, 캔버스의 크기 조정을 위해 사용되는
                                            // 카메라, 또한 월드 스페이스 캔버스를 위한 이벤트 작동에도 사용
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    // 카메라 관련 내용을 처리하기 위해 LateUpdate() 이벤트 핸들러를 사용한다.
    void LateUpdate()
    {
        // 적 캐릭터의 월드 좌표 -> 스크린 좌표
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

        // 적 캐릭터가 카메라 뒤쪽 영역 (180도 회전) 일 때의 좌표값 보정
        if(screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        // 스크린 좌표를 RectTransform 기준 좌표로 변환
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent
                                                                , screenPos
                                                                , uiCamera
                                                                , out localPos);

        // 게이지 위치 변경
        rectHp.localPosition = localPos;

    }
}
