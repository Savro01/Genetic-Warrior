using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMovement : MonoBehaviour
{
    Vector3 direction;
    bool move = false;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            direction = Vector3.forward;
            move = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.back;
            move = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            direction = Vector3.left;
            move = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
            move = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (move)
        {
            rb.MovePosition(this.transform.position + direction);
            move = false;
        }
    }
}
