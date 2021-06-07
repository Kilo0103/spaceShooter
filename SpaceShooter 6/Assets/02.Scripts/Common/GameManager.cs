using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DataInfo; // GameData , Item 클래스를 포함하는 네임스페이스

public class GameManager : MonoBehaviour
{
    //싱글턴 인스턴스에 접근하기 위해 해당 리퍼런스 값을 저장하는 static 타입의 변수를 선언
    //static : 프로그램 실행 중 , ㅇ해당 클래스에 instance라는 이름의 변수는 단 하나만 존재하게 된다.
    public static GameManager isnt = null;

    [Header("Object Pool")]
    public GameObject bulletPrefab; //총알 프리팹
    public int maxPool = 10;        //오브젝트 풀에 생성할 개수
    public List<GameObject> bulletPool = new List<GameObject>();    //관리할 인스턴스를 리스팅

    private bool isPaused; // 일시정지 여부를 판단하는 변수

    public CanvasGroup inventoryCG;

    //킬 카운트
    //[HideInInspector] public int killCount;

    [Header("GameData")]
    //실제 킬 카운트를 표시할 UI 컴포넌트에 대한 리퍼런스 변수
    //public Text killCountTxt;

    //DataManager를 저장할 변수
    private DataManager dataManager;
    public GameDataObject gameData;

    [HideInInspector]
    public int RemainingBullet;

    //실제 킬 카운트를 표시할 UI
    public Text killCountText;

    //인벤토리의 아이템이 변경되었을 때 , 그에 대한 처리를 담당하는 이벤트 관련 설정
    public delegate void ItemChangeDelete();
    public static event ItemChangeDelete OnItemChange; // 이 델리게이트에 연결할 메소드를 시스템적인 이벤트로 인식

    // SlotList 게임오브젝트를 저장할 리퍼런스 변수
    private GameObject slotList;
    // 1. 씬 내부의 SlotList 오브젝트를 Find로 확보
    // 2. SlotList 오브젝트의 자식들(Slot)을 처음부터 끝까지 쫙 흩어서
    // 3. 해당 Slot에 Child(아이템)이 존재하는지 확인
    // ItemList하위의 네 아이템을 저장할 배열
    public GameObject[] itemObjects;

    //아이템을 인벤에 추가 할 처리
    public void AddItem(Item item)
    {
        //인벤토리에 한 종류의 아이템은 하나만 올라간다
        //기존 보유 아이템과 종류가 중복되면 추가하지 않음
        if (gameData.equipItem.Contains(item)) { return; }
        //신규 요소면 equipItem에 추가
        gameData.equipItem.Add(item);

        //아이템 데이터 요소 : 타입, 계산 방식
        //아이템 타입 개수가 적으므로, 교재에서는 switch 방식으로 처리
        //Dictionary<string, Item> items = new Dictionary<string, Item>();
/*      Dictionary<Item.ItemType, Item> items = new Dictionary<Item.ItemType, Item>();
        items.Add(Item.ItemType.HP, new Item());
        Item i = items[Item.ItemType.HP];
*/
        switch (item.itemType)
        {
            case Item.ItemType.HP:
                if(item.itemCalc == Item.ItemCalc.INC_VALUE)
                {
                    gameData.hp += item.value;
                } else
                {
                    gameData.hp += gameData.hp * item.value;
                }
                break;
            case Item.ItemType.DAMAGE:
                if (item.itemCalc == Item.ItemCalc.INC_VALUE)
                {
                    gameData.damage += item.value;
                }
                else
                {
                    gameData.damage += gameData.damage * item.value;
                }
                break;
            case Item.ItemType.SPEED:
                if (item.itemCalc == Item.ItemCalc.INC_VALUE)
                {
                    gameData.speed += item.value;
                }
                else
                {
                    gameData.speed += gameData.speed * item.value;
                }
                break;
            case Item.ItemType.GRENADE:

                break;
        }
        UnityEditor.EditorUtility.SetDirty(gameData);

        // 아이템 변경을 실시간으로 적용시키기 위해, 정의해 둔 이벤트를 발생시킨다
        OnItemChange();
    }
    //아이템을 인벤에 제거 할 처리
    public void RemoveItem(Item item)
    {
        //해당 아이템을 gameData의 아이템리스트에서 삭제
        gameData.equipItem.Remove(item);

        //아이템 종류에 따라 처리
        switch (item.itemType)
        {
            case Item.ItemType.HP:
                if (item.itemCalc == Item.ItemCalc.INC_VALUE)
                {
                    gameData.hp -= item.value;
                }
                else
                {
                    gameData.hp = gameData.hp / (1.0f + item.value);
                }
                break;
            case Item.ItemType.DAMAGE:
                if (item.itemCalc == Item.ItemCalc.INC_VALUE)
                {
                    gameData.damage -= item.value;
                }
                else
                {
                    gameData.damage = gameData.damage / (1.0f + item.value);
                }
                break;
            case Item.ItemType.SPEED:
                if (item.itemCalc == Item.ItemCalc.INC_VALUE)
                {
                    gameData.speed -= item.value;
                }
                else
                {
                    gameData.speed = gameData.speed / (1.0f + item.value);
                }
                break;
            case Item.ItemType.GRENADE:

                break;
        }
        UnityEditor.EditorUtility.SetDirty(gameData);
        // 아이템 변경을 실시간으로 적용시키기 위해, 정의해 둔 이벤트를 발생시킨다
        OnItemChange();
    }

    // 적을 죽일 떄 킬 카운트를 증가시킨 후 저장 메소드
    public void IncreaseKillCount()
    {
        //++killCount;
        //killCountText.text = "KILL" + killCount.ToString("0000");

        //킬 카운트 저장
        //PlayerPrefs.SetInt("KILL_COUNT", killCount);

        //남은 잔탄도 같이 저장
        //PlayerPrefs.SetInt("REMAINING_BULLET", RemainingBullet);

        ++gameData.killCount;
        killCountText.text = "KILL" + gameData.killCount.ToString("0000");
    }

    public void LoadGameData()
    {
        //killCount = PlayerPrefs.GetInt("KILL_COUNT", 0);
        //RemainingBullet = PlayerPrefs.GetInt("REMAINING_BULLET", 0);
        //killCountText.text = "KILL" + killCount.ToString("0000");


        //DataManager를 통해 대신할 로직
        //GameData data = dataManager.Load();
        //gameData.hp = data.hp;
        //gameData.damage = data.damage;
        //gameData.speed = data.speed;
        //gameData.killCount = data.killCount;
        //gameData.equipItem = data.equipItem;

        if (gameData.equipItem.Count > 0)
        {
            InventorySetup();
        }
        killCountText.text = "KILL" + gameData.killCount.ToString("0000");
    }

    void InventorySetup()
    {
        // SlotList 하위에 있는 모든 Slot을 추출
        var slots = slotList.GetComponentsInChildren<Transform>();
        //n번 아이템에 대해 수행
        for(int idx = 0; idx < gameData.equipItem.Count; idx++)
        {
            //인벤토리 UI에 있는 Slot 개수만큼 반복
            for(int j = 1; j < slots.Length; j++) //슬롯이 비어있는지 순서대로 확인
            {
                if (slots[j].childCount > 0) { continue; } // 이 슬롯에 뭔가 아이템이 이미 적재되어 있다면 패스

                // 보유아이템의 종류에 따라 인덱스 확보
                int itemIndex = (int)gameData.equipItem[idx].itemType;

                //아이템의 부모를 Slot 게임오브젝트로 변경
                itemObjects[itemIndex].GetComponent<Transform>().SetParent(slots[j]);

                //아이템이 가지는 ItemInfo 정보를 적용시킴
                itemObjects[itemIndex].GetComponent<ItemInfo>().itemData = gameData.equipItem[idx];

                break;
            }
        }
    }

    //게임 데이터 저장
    public void SaveGameData()
    {
        //현재 활성화 되어 있는 GameData 인스턴스의 내용을 파일에 저장한다
        //dataManager.Save(gameData);
        UnityEditor.EditorUtility.SetDirty(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGameData(); // 종료 시, 파일에 현재의 GameData를 저장
    }


    public void OnInventoryOpen(bool isOpened)
    {
        inventoryCG.alpha = (isOpened) ? 1.0f : 0.0f;
        inventoryCG.interactable = isOpened;
        inventoryCG.blocksRaycasts = isOpened;
    }

    // 일시 정지 버튼 클릭시 호출할 이벤트 핸들러 메소드
    public void OnPauseClick()
    {
        isPaused = !isPaused;

        // 3항 연산을 이용하여, Time.timeScale 값을 0 / 1로 설정
        // Time.timeScale : 0 이면 정지 1이면 정상화
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        // 플레이어 오브젝트의 이벤트도 막아야 함
        var playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        var scripts = playerObj.GetComponents<MonoBehaviour>();
        foreach(var script in scripts)
        {
            script.enabled = !isPaused;
        }
        var canvasGroup = GameObject.Find("Panel - Weapon").GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = !isPaused;
    }

    //오브젝트 풀에 총알을 생성하는 함수
    public void CreatePooling()
    {
        //여기서, 탄환 여러개를 미리 만들어서 리스트에 묶어둘 것이다
        GameObject objectPools = new GameObject("ObjectPools"); //이 오브젝트를 가상의 부모 오브젝트로 설정해서, 오브젝트 풀로 생성한 오브젝트들이 하이라키에 무작위 등장하는 것을 방지

        for(int i = 0; i < maxPool; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab, objectPools.transform);

            obj.name = "Bullet_" + i.ToString("00");
            obj.SetActive(false); //생성한 오브젝트 일단 비활성

            bulletPool.Add(obj); // 생성한 오브젝트를 리스트에 연결
        }
    }

    public GameObject GetBullet()
    {
        for(int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].activeSelf == false)
            {
                return bulletPool[i];
            }
        }

        return null;
    }

    private void Awake()
    {
        if(isnt == null)
        {
            //instance가 null : 아직, 이 변수를 한 번도 사용한 적이 없다.
            //그럴 경우 , 지금 생성된 자기 자신의 리퍼런스를 instance에 저장
            isnt = this;
        } else if(isnt != this)
        {
            Destroy(this.gameObject);
        }

        //다른 씬이 로드 되더라도, 지금 이 인스턴스를 파괴하지 않음
        DontDestroyOnLoad(this.gameObject);

        //DataManager이용을 위한 준비
        dataManager = GetComponent<DataManager>();
        dataManager.Initialized(); //초기화

        //SlotList 오브젝트 획득
        slotList = inventoryCG.transform.Find("SlotList").gameObject;

        // 게임 첫 실행 시
        LoadGameData();

        //탄환에 대한 오브젝트 풀 생성
        CreatePooling();
    }

    //적 캐릭터가 출현할 위치를 담은 배열
    public Transform[] points; //이것도 , 이전에 WayPoint 처리했던 것처럼 랜덤으로 좌표를 설정할 수도 있음
    //적 캐릭터 Prefab을 저장할 변수
    public GameObject enemy;
    //적 캐릭터 생성 주기
    public float createTime = 2.0f;
    //적 캐릭터의 최대 생성 개수
    public int maxEnemy = 6;
    //게임 종료 여부를 판단할 변수
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {

        OnInventoryOpen(false);

        //"SpawnPointGroup"의 자식 오브젝트들이 가지고 있는 컴포넌트 중 <Transform> 컴포넌트를 가져옴
        points = GameObject.Find("spawnPointGroup").GetComponentsInChildren<Transform>();

        if(points.Length > 0)
        {
            //적을 생성하는 메소드를 코르틴으로 별도 작동시킴
            StartCoroutine(this.CreateEnemy());
        }
    }

    IEnumerator CreateEnemy()
    {
        while (!isGameOver) {
            int enemyCount = GameObject.FindGameObjectsWithTag("ENEMY").Length;
            //적 캐릭터 수가 최대보다 작을 때만 실행
            if (enemyCount < maxEnemy)
            {
                //생성했다면, 연속 생성을 막기 위해 지정시간만큼 대기하도록 한다
                yield return new WaitForSeconds(createTime);
                // 랜덤으로 인덱스 설정 from points 배열
                int idx = Random.Range(1, points.Length);
                // 적 캐릭터 오브젝트 생성
                Instantiate(enemy, points[idx].position, points[idx].rotation);
            }
            else
            {
                //일단 안 만들어도 넘기는 것
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
