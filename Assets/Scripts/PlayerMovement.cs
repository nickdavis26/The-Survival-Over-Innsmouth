using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(1, 10)]
    public float speed = 10;
    public float sprintingMultiplier = 10.2f;
    public float jumpSpeed;
    public UnityEngine.AI.NavMeshAgent nav;
    public GameObject cam1;
    Vector3 moveForward;
    Vector3 moveRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveForward = cam1.transform.TransformDirection(Vector3.forward);
        moveRight = cam1.transform.TransformDirection(Vector3.right);
        if (Input.GetKey(KeyCode.W))
        {
            nav.Move(moveForward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            nav.Move(-moveForward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            nav.Move(-moveRight * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            nav.Move(moveRight * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            nav.Move(moveForward * sprintingMultiplier * Time.deltaTime);
        }

    }
}