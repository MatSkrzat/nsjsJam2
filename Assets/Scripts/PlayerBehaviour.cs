using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [SerializeField]
    CinemachineVirtualCamera vCamera;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float jumpForce = 20f;

    private float horizontalAxis = 0f;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        float newHorizontalAxis = Input.GetAxis("Horizontal") * speed;
        if (!isDead)
        {
            //jumping
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
            //moving left and right
            if (newHorizontalAxis != horizontalAxis)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            rb.AddForce(new Vector2(newHorizontalAxis, 0));

            //animating movement
            animator.SetFloat("HorizontalAxis", newHorizontalAxis);
            if (newHorizontalAxis != 0)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Light")
        {
            isDead = true;
            animator.SetBool("IsDead", true);
            vCamera.Follow = null;
            Debug.Log("GAME OVER SUCKER");
        }
    }
}
