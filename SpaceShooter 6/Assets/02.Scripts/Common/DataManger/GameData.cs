using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DataInfo
{
    [System.Serializable]
    public class GameData // 바이너리 파일 형식으로 저장될 것을 전제로 함
    {
        public int killCount = 0;
        public float hp = 120.0f;
        public float damage = 25.0f;
        public float speed = 6.0f;
        public List<Item> equipItem = new List<Item>(); //소지중인 아이템 리스트
    }

    [System.Serializable]
    public class Item
    {
        public enum ItemType { HP, SPEED, GRENADE, DAMAGE }
        public enum ItemCalc { INC_VALUE, PERCENT }

        public ItemType itemType;
        public ItemCalc itemCalc;
        public string name;
        public string desc;
        public float value;

    }

    // 아이템 데이터 설계시
    // 어떤 타입의 아이템이 존재하는 가
    // 각 아이템이 어떠한 방식으로 사용되는가
    // 아이템이 1회용인가 지속형인가
    // 아이템 사용 시 어떤 데이터가 어떤 식으로 영향을 받는가
}
