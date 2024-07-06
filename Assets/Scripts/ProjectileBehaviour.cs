using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    Vector2 target;
    GameObject targetGameObject;
    float speed = 50f;

    bool canMove = false;

    void FixedUpdate()
    {
        if (target != null && canMove)
        {
            transform.position = Vector2.Lerp(transform.position, target, speed * Time.deltaTime);

        }
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    void EnableMoving()
    {
        canMove = true;
        target = targetGameObject.transform.position;
        Invoke("DestroyProjectile", 3f);
    }

    public void StartMovingTowardsTarget(GameObject target, float speed = 50f, float shootDelay = 0f)
    {
        targetGameObject = target;
        this.speed = speed;
        Invoke("EnableMoving", shootDelay);
    }
}
