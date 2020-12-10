using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionControl : MonoBehaviour
{

    private Animator animator;


    //애니메이터 컨트롤러에 정의한 파라미터의 해시값을 미리 추출
    private readonly int hashFire = Animator.StringToHash("Fire");
    private readonly int hashReload = Animator.StringToHash("Reload");
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Dying");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");


    private Transform playerTr;
    private Transform EnemyTr;
    float d = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EnemyTr = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        d = Vector3.Distance(playerTr.position, EnemyTr.position);

        if (d > 5.0f)
        {
            animator.SetBool("IsMove", true);
            animator.SetFloat("Speed", 4.0f);
        }
        else
        {
            animator.SetBool("IsMove", true);
            animator.SetFloat("Speed", 2.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(hashFire);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetInteger(hashDieIdx, Random.Range(0, 3));
            animator.SetTrigger(hashDie);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetInteger(hashDieIdx, 0);
            animator.SetTrigger(hashDie);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            animator.SetInteger(hashDieIdx, 1);
            animator.SetTrigger(hashDie);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetInteger(hashDieIdx, 2);
            animator.SetTrigger(hashDie);
        }
    }
}
