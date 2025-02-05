using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int playerLives = 3;

    private bool isPlayerAlive = true;
    public bool isPaused = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    public GameObject pauseMenu;
    public GameObject GameOverUI;
    public GameObject Win;

    public static GameManager _instance{ get; private set;}

    [SerializeField] public Vector3 spawnPoint;
    [SerializeField] private GameObject player;
    //[SerializeField] private GameObject Playerlivesandscore;
    [SerializeField] AudioClip DeathSFX;
    [SerializeField] [Range(0f, 1f)] float Dead = .5f;




    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }

            return _instance;
        }

    }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
            
        else
        {
            _instance = this;
        }
        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);
    }
    
    
    void Start()
    {
        

        if (_instance != this)
        {
            return;
        }

        score = 0;
        playerLives = 3;

        scoreText.text = "Score: " + score;

        livesText.text = "Lives: " + playerLives;

        spawnPoint = player.transform.position;

        
    }

    
    void Update()
    {
        paused();
        QuitEscape();
        
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    public void paused()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // Unpause the game
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 0)
        {
            TakeLife(); //run code and reduce life by 1
        }
        else
        {
            isPlayerAlive = false;
            GameOver();
        }
    }

    

    void TakeLife()
    {
        if(playerLives > 0)
        {
            playerLives = playerLives - 1;
            livesText.text = "Lives: " + playerLives.ToString();

            if (playerLives > 0)
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
                player.transform.position = spawnPoint;
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            GameOver();
        }

    }


    public void GameOver()
    {
        if(playerLives == 0)
        {
            PlayClip(DeathSFX, Dead);
            GameOverUI.SetActive(true);
            isPlayerAlive = false;
            isPaused = true;
            Time.timeScale = isPaused ? 0 : 1;
            //Playerlivesandscore.SetActive(false);
            CancelInvoke();
        }
    }

    public void Winner()
    {
        if(playerLives > 0)
        {
            Win.SetActive(true);
            isPlayerAlive = true;
            CancelInvoke();
        }
        
    }

    public void restart()
    {
        score = 0;
        playerLives = 3;

        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + playerLives;

        GameOverUI.SetActive(false);
        Win.SetActive(false);
        SceneManager.LoadScene(1);
    }


    public void mainMenu()
    {

        restart();
       
        Time.timeScale = 1;

        Debug.Log("Loading main menu");
        SceneManager.LoadScene("Menu");

        
        
        pauseMenu.SetActive(false);
        GameOverUI.SetActive(false);
        Win.SetActive(false);
        isPaused = false;
    }

    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }

    public void Quit()
    {  
            Debug.Log("Quit");
            Application.Quit();
        
    }
    public void QuitEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}