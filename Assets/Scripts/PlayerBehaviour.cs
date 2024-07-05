using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D rb = new Rigidbody2D();
    Animator animator = new Animator();

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float jumpForce = 20f;

    private float horizontalAxis = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //jumping
        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector2.up * jumpForce);
        }

        //moving left and right
        float newHorizontalAxis = Input.GetAxis("Horizontal") * speed;
        if (newHorizontalAxis != horizontalAxis) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        rb.AddForce(new Vector2(newHorizontalAxis, 0));

        //animating movement
        animator.SetFloat("HorizontalAxis", newHorizontalAxis);
        if (newHorizontalAxis != 0) {
            animator.SetBool("IsMoving", true);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.tag == "Light") 
        {
            Debug.Log("GAME OVER SUCKER");
        }
    }
}
