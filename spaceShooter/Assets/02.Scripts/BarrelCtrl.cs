using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public Mesh[] meshes;
    //MeshFilter 컴포넌트를 저장할 변수
    private MeshFilter meshFilter;
    //MeshRenderer 컴포넌트를 저장할 변수
    private MeshRenderer _renderer;

    //AudioSource 컴포넌트를 저장할 변수
    private AudioSource _audio;

    //폭발 효과 프리팹을 저장할 변수
    public GameObject expEffect;
    private int hitCount = 0;
    private Rigidbody rb;
    public Texture[] textures;

    //폭발 반경
    public float expRadius = 10.0f;
    //폭발음 오디오 클립
    public AudioClip expSfx;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision coll)
    {
        //충돌시 태그확인
        if (coll.collider.CompareTag("BULLET")) // coll.collider.tag ==
        {
            //총알의 충돌 횟수가 3발인지 체크
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        //폭발 효과 프리팹을 동적으로 생성
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);
        //Rigidbody 컴포넌트의 mass를 1.0으로 수정해 무게를 가볍게 함
        rb.mass = 5.0f;
        //위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * 1000.0f);
        //폭발력 생성
        IndirectDamage(transform.position);
        //난수를 발생시킴
        int idx = Random.Range(0, meshes.Length);
        //찌그러진 메쉬를 적용
        meshFilter.sharedMesh = meshes[idx];
        GetComponent<MeshCollider>().sharedMesh = meshes[idx];
        
    }

    void IndirectDamage(Vector3 pos)
    {
        //구 범위로 주변에 있는 드럼통을 찾아 변수에 저장(?)
        Collider[] colls = Physics.OverlapSphere(pos, expRadius, 1 << 8);
        //Collider[] colls = Physics.OverlapSphere(pos, expRadius);

        foreach(var coll in colls)
        {
            //폭발 범위에 포함된 드럼통의 rigidbody 컴포넌트 추출
            var _rb = coll.GetComponent<Rigidbody>();
            if (_rb != null)
            {
                //드럼통의 무게를 가볍게 함
                _rb.mass = 5.0f;
                //폭발력을 전달
                _rb.AddExplosionForce(1200.0f, pos, expRadius, 1000.0f);
            }
        }
        //폭발음 발생
        _audio.PlayOneShot(expSfx, 1.0f);
    }
}
