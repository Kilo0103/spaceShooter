using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform itemTr;
    private Transform inventoryTr;

    private Transform itemListTr;
    private CanvasGroup canvasGroup;

    public static GameObject draggingItem = null; //현재 드래그 중인 아이템의 정보를 가지는 게임 오브젝트 리퍼런스

    // Start is called before the first frame update
    void Start()
    {
        itemTr = GetComponent<Transform>();

        inventoryTr = GameObject.Find("Inventory").GetComponent<Transform>();

        itemListTr = GameObject.Find("ItemList").GetComponent<Transform>();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 이벤트가 발생한 오브젝트(즉, 내가 지금 끌고가려는 아이템)
        // 좌표를 마우스 좌표에 맞춘다(즉, 마우스로 끌고가는 것처럼 보여준다)
        itemTr.position = Input.mousePosition;
    }

    //드래그가 시작하는 시점에 실행되는 이벤트 핸들러 메소드
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventoryTr);

        // 드래그가 시작되면, 해당 아이템의 정보를 저장
        draggingItem = this.gameObject;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 행동이 끝나면, 초기화 하여 오동작 방지
        draggingItem = null;

        canvasGroup.blocksRaycasts = true;

        //슬롯 위에 드랍된 것이 아니면 되돌리기
        if (itemTr.parent == inventoryTr) { 
            itemTr.SetParent(itemTr.transform);

            //슬롯에 추가된 아이템 갱신을 알림
            GameManager.isnt.RemoveItem(GetComponent<ItemInfo>().itemData);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
