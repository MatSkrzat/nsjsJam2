using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KingChildren
{
    shootingGameObject,
    carousel
}

public class KingBehaviour : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject playerGameObject;
    public List<GameObject> livesGameObjects;
    Vector2 projectileInstantiationPosition;
    public float shootFrequency = 1f;
    public float phaseTwoShootFrequency = 1.5f;
    public float projectileSpeed = 50f;
    public int phaseTwoProjectileQuantity = 5;
    public bool isAtPhaseTwo = false;
    public int livesAmount = 4;

    // Start is called before the first frame update
    void Start()
    {
        projectileInstantiationPosition = gameObject.transform.GetChild((int)KingChildren.shootingGameObject).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetChild((int)KingChildren.carousel).transform.Rotate(Vector3.forward * Time.deltaTime * 50f);
    }

    public void InstantiateProjectile(float delay = 0f)
    {
        var projectileGameObject = Instantiate(projectilePrefab, projectileInstantiationPosition, Quaternion.identity);
        projectileGameObject.GetComponent<ProjectileBehaviour>().StartMovingTowardsTarget(playerGameObject, projectileSpeed, delay);
    }

    public void ShootAtPlayer()
    {
        if (GameManager.instance.isGameOver || !GameManager.instance.isGameStarted)
        {
            return;
        }
        if (isAtPhaseTwo)
        {
            PhaseTwoShoot();
        }
        else
        {
            InstantiateProjectile();
        }

    }

    public void DamageKing()
    {
        if (livesAmount > 0) {
            livesGameObjects[livesAmount - 1].SetActive(false);
            livesAmount--;
        }
        else {
            GameManager.instance.GameOver();
        }
    }

    public void PhaseTwoShoot()
    {
        for (int i = 1; i <= phaseTwoProjectileQuantity; i++)
        {
            InstantiateProjectile(i * 0.25f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player" && GameManager.instance.isGameStarted)
        {
            InvokeRepeating("ShootAtPlayer", 1f, isAtPhaseTwo ? phaseTwoShootFrequency : shootFrequency);
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
