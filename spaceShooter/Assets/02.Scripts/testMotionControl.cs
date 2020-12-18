using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMotionControl : MonoBehaviour
{

    private Animator animator;


    //애니메이터 컨트롤러에 정의한 파라미터의 해시값을 미리 추출
    private readonly int hashFire = Animator.StringToHash("Shoot");
    private readonly int hashReload = Animator.StringToHash("Reload");
    private readonly int hashMove = Animator.StringToHash("IsTransfer");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Dying");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetFloat(hashSpeed, 0.0f);
            animator.SetBool(hashMove, false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetFloat(hashSpeed, 2.0f);
            animator.SetBool(hashMove, true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetFloat(hashSpeed, 6.0f);
            animator.SetBool(hashMove, true);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetFloat(hashSpeed, 6.0f);
            animator.SetBool(hashMove, true);
            animator.SetTrigger(hashFire);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetFloat(hashSpeed, 2.0f);
            animator.SetBool(hashMove, true);
            animator.SetTrigger(hashReload);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetInteger(hashDieIdx, 2);
            animator.SetTrigger(hashDie);
        }
    }
}
