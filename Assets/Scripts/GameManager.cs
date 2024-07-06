using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
        PlayerPrefs.SetInt("deaths", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        isGameOver = true;
        int deaths = PlayerPrefs.GetInt("deaths");
        deaths++;
        PlayerPrefs.SetInt("deaths", deaths);
        PlayerPrefs.Save();
        ui.ShowGameOverPanel(deaths);
    }

    public void GameFinished() {
        isGameStarted = false;
        isGameOver = true;
        ui.ShowGameFinishedPanel();
    }

    public void SetLastCheckpoint(GameObject checkpointObject)
    {
        lastCheckpoint = checkpointObject.transform.position;
        checkpointObject.GetComponent<Light2D>().enabled = true;
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
