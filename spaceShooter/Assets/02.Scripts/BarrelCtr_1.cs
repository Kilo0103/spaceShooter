using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtr_1 : MonoBehaviour
{
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
