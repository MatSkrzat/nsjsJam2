using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Vector2 lastCheckpoint = new Vector2(PlayerPrefs.GetFloat("checkpoint_x"), PlayerPrefs.GetFloat("checkpoint_y"));
        if (lastCheckpoint != Vector2.zero)
        {
            player.transform.position = lastCheckpoint;
            isNewGame = false;
        }
        else
        {
            isNewGame = true;
        }
    }

    public UIManager ui;
    public GameObject player;
    public GameObject king;
    public Vector2 lastCheckpoint;
    public bool isGameStarted = false;
    public bool isGameOver = false;
    public bool isNewGame = true;
    public List<GameObject> checkpoints = new List<GameObject>();
    PlayerBehaviour playerBehaviour;
    KingBehaviour kingBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        kingBehaviour = king.GetComponent<KingBehaviour>();
    }

    public void StartGame()
    {
        isGameStarted = true;
        playerBehaviour.StartMoving();
    }

    public void ResetGame()
    {
        PlayerPrefs.SetFloat("checkpoint_x", 0);
        PlayerPrefs.SetFloat("checkpoint_y", 0);
        PlayerPrefs.SetInt("checkpoint_id", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        isGameOver = true;
        ui.ShowGameOverPanel();
    }

    public void SetLastCheckpoint(GameObject checkpointObject)
    {
        lastCheckpoint = checkpointObject.transform.position;
        PlayerPrefs.SetFloat("checkpoint_x", lastCheckpoint.x);
        PlayerPrefs.SetFloat("checkpoint_y", lastCheckpoint.y);
        PlayerPrefs.SetInt("checkpoint_id", checkpoints.Count);
        PlayerPrefs.Save();
    }

    public void ReloadGame()
    {
        CallOnReload();
        SceneManager.LoadScene(0);
    }

    void CallOnReload()
    {
        isGameOver = false;
        isGameStarted = false;
    }
}
