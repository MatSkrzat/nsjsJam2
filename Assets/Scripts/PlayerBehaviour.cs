using Cinemachine;
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
    void FixedUpdate()
    {
        float newHorizontalAxis = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime;
        if (!isDead && GameManager.instance.isGameStarted)
        {
            //jumping
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime);
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

    public void StartMoving()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void StopMoving()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Light")
        {
            isDead = true;
            animator.SetBool("IsDead", true);
            StopMoving();
            GameManager.instance.sm.PlaySingleSound(GameManager.instance.sm.Explosion);
            GameManager.instance.GameOver();
        }
        else if (collider.tag == "Checkpoint")
        {
            GameManager.instance.sm.PlaySingleSound(GameManager.instance.sm.Rune);
            GameManager.instance.SetLastCheckpoint(collider.gameObject);
        }
    }

    public void ResetPlayerAtPosition(Vector2 position)
    {
        animator.SetBool("IsDead", false);
        isDead = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.localScale = Vector3.one;
        transform.position = position;
    }
}
