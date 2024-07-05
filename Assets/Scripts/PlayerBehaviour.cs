using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D rb = new Rigidbody2D();

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float jumpForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void FixedUpdate()
    {

    }
}
