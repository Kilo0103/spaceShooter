using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl_2 : MonoBehaviour
{
    public enum WeaponType
    {
        RIFLE = 0,
        SHOTGUN
    }
    //주인공이 들고 있는 무기를 저장할 변수
    public WeaponType currWeapon = WeaponType.RIFLE;

    //총알 프리팹
    public GameObject bullet;
    //총알 발사좌표
    public Transform firePos;
    //탄피 추출 파티클
    public ParticleSystem cartridge;
    //총구 화염 파티클
    public ParticleSystem muzzleFlash;
    //총기 소리
    private AudioSource _audio;
    //오디오 클립
    public PlayerSfx playerSfx;

    // Start is called before the first frame update
    void Start()
    {
        //절대강좌에는 있지만 없어야 작동해서 일단 남겨놓음
        //muzzleFlash = firePos.GetComponent<ParticleSystem>();


        _audio = GetComponent<AudioSource>();
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
        FireSfx();
    }

    void FireSfx()
    {
        //현재 들고 있는 무기의 오디오 클립을 가져옴
        var _sfx = playerSfx.fire[(int)currWeapon];
        //총기 격발음 실행
        _audio.PlayOneShot(_sfx, 1.0f);
    }
}
