using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl_2 : MonoBehaviour
{
    //총알 프리팹
    public GameObject bullet;
    //총알 발사좌표
    public Transform firePos;
    //탄피 추출 파티클
    public ParticleSystem cartridge;
    //총구 화염 파티클
    public ParticleSystem muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        //절대강좌에는 있지만 없어야 작동해서 일단 남겨놓음
        //muzzleFlash = firePos.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        //Bullet 프리팹을 동적으로 생성
        Instantiate(bullet, firePos.position, firePos.rotation);
        //파티클 실행
        cartridge.Play();
        //총구 화염 실행
        muzzleFlash.Play();
    }
}
