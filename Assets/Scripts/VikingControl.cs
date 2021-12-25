using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]

public class VikingControl : MonoBehaviour
{
    private float movingSpeed = 10f, startTime;
    Rigidbody rb;
    Animator animator;
    private Vector3 direction = Vector3.forward;
    public float JumpingForce;
    private bool jump, run;
    public static int score;
    public static int totalTime;
    void Awake()
    {
        Debug.Log("awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        score = 0;
        jump = false;
        run = true;
        startTime = Time.time;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Coin") 
        {
            Destroy(collider.gameObject);
            score++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Tile") {
            jump = false;
        }
        else if (collision.transform.tag == "Block") 
        {
            run = false;
            jump = false;
            movingSpeed = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Tile")
        {
            jump = false;
        }
        else if (collision.transform.tag == "Block")
        {
            run = false;
            jump = false;
            movingSpeed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("update");

        if (transform.position.y < -2) 
        {
            float endTime = Time.time;
            totalTime = (int)(endTime - startTime);
            Score.ToScore();
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (direction == Vector3.forward) direction = Vector3.left;
            else if (direction == Vector3.left) direction = Vector3.back;
            else if (direction == Vector3.back) direction = Vector3.right;
            else if (direction == Vector3.right) direction = Vector3.forward;
            transform.Rotate(Vector3.up, -90);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (direction == Vector3.forward) direction = Vector3.right;
            else if (direction == Vector3.right) direction = Vector3.back;
            else if (direction == Vector3.back) direction = Vector3.left;
            else if (direction == Vector3.left) direction = Vector3.forward;
            transform.Rotate(Vector3.up, 90);
        }

        transform.localPosition += movingSpeed * Time.deltaTime * direction;

        if (!jump && Input.GetKey(KeyCode.Space)) {
            rb.AddForce(JumpingForce * Vector3.up);
            jump = true;
        }

        animator.SetBool("Run", run);
        animator.SetBool("Jump", jump);

    }
}
