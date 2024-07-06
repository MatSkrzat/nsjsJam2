using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBehaviour : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject playerGameObject;
    Vector2 projectileInstantiationPosition;
    public float shootFrequency = 1f;
    public float projectileSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        projectileInstantiationPosition = gameObject.transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootAtPlayer()
    {
        var projectileGameObject = Instantiate(projectilePrefab, projectileInstantiationPosition, Quaternion.identity);
        projectileGameObject.GetComponent<ProjectileBehaviour>().StartMovingTowardsTarget(playerGameObject.transform.position, projectileSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            InvokeRepeating("ShootAtPlayer", 1f, shootFrequency);
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            CancelInvoke("ShootAtPlayer");
        }
    }

}
