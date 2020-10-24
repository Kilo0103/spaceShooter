using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtr_4 : MonoBehaviour
{
    public Mesh[] meshes;
    //MeshFilter 컴포넌트를 저장할 변수
    private MeshFilter meshFilter;
    //MeshRenderer 컴포넌트를 저장할 변수
    private MeshRenderer _renderer;

    private Rigidbody rb;
    //폭발 효과 프리팹을 저장할 변수
    public GameObject expEffect;
    private int hitCount = 0;

    public Texture[] textures;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
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
        rb.mass = 1.0f;
        //위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * 1000.0f);
        //난수를 발생시킴
        int idx = Random.Range(0, meshes.Length);
        //찌그러진 메쉬를 적용
        meshFilter.sharedMesh = meshes[idx];
        GetComponent<MeshCollider>().sharedMesh = meshes[idx];
    }
}
