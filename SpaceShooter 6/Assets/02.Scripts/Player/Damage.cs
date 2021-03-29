﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private const string enemyTag = "ENEMY";

    private float initHp = 100.0f;
    public float currHp;

    //델리게이트 및 이벤트 선언
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        currHp = initHp;
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == bulletTag)
        {
            Destroy(coll.gameObject);
            currHp -= 5.0f;
            Debug.Log("Player HP = " + currHp.ToString());

            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}