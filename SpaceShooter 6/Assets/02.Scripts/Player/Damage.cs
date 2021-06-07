using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private const string enemyTag = "ENEMY";

    private float initHp = 100.0f;
    public float currHp;

    //블러드 스크린 텍스처의 리퍼런스를 저장하기 위한 변수
    public Image bloodScreen;

    // Hp 바 이미지에 대한 리퍼런스를 저장하기 위한 변수
    public Image hpbar;
    //생명 게이지의 첫 색상(녹색)
    private readonly Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    private Color currColor; //실행 중의 색상 (currentColor)

    //델리게이트 및 이벤트 선언
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        initHp = GameManager.isnt.gameData.hp;
        currHp = initHp;

        hpbar.color = initColor;
        currColor = initColor;
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == bulletTag)
        {
            Destroy(coll.gameObject);
            StartCoroutine(ShowBloodScreen());
            currHp -= 5.0f;
            Debug.Log("Player HP = " + currHp.ToString());

            //생명 게이지의 색상 및 크기를 처리하는 메소드 실행
            DisplayHpbar();

            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    IEnumerator ShowBloodScreen()
    {
        //BloodScreen 텍스처의 알파값을 불규칙하게 변경
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        //BloodScreen 텍스처의 색상을 모두 0으로 변경
        bloodScreen.color = Color.clear;
    }

    void PlayerDie()
    {
        GameManager.isnt.isGameOver = true;

        OnPlayerDie();

        /*
        
        Debug.Log("PlayerDie !");
        //"ENEMY"태그로 지정된 모든 적 캐릭터를 추출해 배열에 저장
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //배열의 처음부터 순회하면서 적 캐릭터의 OnPlayerDie 함수를 호출
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SendMessage("OnPlayerDIe", SendMessageOptions.DontRequireReceiver);
        }

        */
    }

    void DisplayHpbar()
    {
        if ((currHp / initHp) > 0.5f)
            currColor.r = (1 - (currHp / initHp)) * 2.0f;
        else //생명 수치가 0%일 때까지는 노란색에서 빨간색으로 변경
            currColor.g = (currHp / initHp) * 2.0f;

        //Hpbar의 색상 변경
        hpbar.color = currColor;
        //BpBar의 크기 변경
        hpbar.fillAmount = (currHp / initHp);
    }

    private void OnEnable()
    {
        GameManager.OnItemChange += UpdateSetup;
    }
    void UpdateSetup()
    {
        initHp = GameManager.isnt.gameData.speed;
        currHp = GameManager.isnt.gameData.hp - currHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
