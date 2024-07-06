using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    Vector2 target;
    float speed = 50f;

    void FixedUpdate()
    {
        if (target != null)
        {
            Debug.Log("MOVE");
            transform.position = Vector2.Lerp(transform.position, target, speed * Time.deltaTime);

        }
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void StartMovingTowardsTarget(Vector2 target, float speed = 50f)
    {
        this.target = target;
        this.speed = speed;
        Invoke("DestroyProjectile", 3f);
    }
}
