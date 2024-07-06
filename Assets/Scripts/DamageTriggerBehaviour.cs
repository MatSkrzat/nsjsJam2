using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerBehaviour : MonoBehaviour
{
    Animator animator;
    public GameObject king;
    KingBehaviour kingBehaviour;
    public bool isTouched;
    void Start() {
        animator = GetComponent<Animator>();
        kingBehaviour = king.GetComponent<KingBehaviour>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && !isTouched)
        {
            animator.SetBool("Touched", true);
            isTouched = true;
            kingBehaviour.DamageKing();
        }
    }
}
