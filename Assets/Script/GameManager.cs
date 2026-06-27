 using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject m_PausePanel;
    public GameObject m_GameOverPanel;
    public GameObject m_WinPanel;

    public Button m_ResumeButton;
    public Button m_MainMenuButton;
    public Button m_ReStartButton;
    public Button m_QuitButton;

    public bool m_IsWin = false;
    public bool m_IsGameOver = false;


    public Slider m_HealthSlider;
    public TMP_Text m_CoinText;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        m_PausePanel = GameObject.Find("Pause");
        m_GameOverPanel = GameObject.Find("GameOver");
        m_WinPanel = GameObject.Find("Win");


        m_ResumeButton = GameObject.Find("Resume").GetComponent<Button>();
        m_MainMenuButton = GameObject.Find("MainMenu").GetComponent<Button>();
        m_ReStartButton = GameObject.Find("Restart").GetComponent<Button>();

        m_CoinText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
        m_HealthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();

        UpdateCoin();
        UpdateHealthBar();
    }


    public void UpdateCoin()
    {
        m_CoinText.text = Player.instance.m_Coin.ToString();
    }

    public void UpdateHealthBar()
    {
        m_HealthSlider.value = Player.instance.m_CurrentHealth / Player.instance.m_MaxHealth;
    }

    private void Start()
    {
        m_ResumeButton.onClick.AddListener(() =>
        {

            ResumeGame();
        });

        m_ReStartButton.onClick.AddListener(() =>
        {

            RestartGame();
        });

        m_MainMenuButton.onClick.AddListener(() =>
        {
            GoMainMenu();
        });

        //m_QuitButton.onClick.AddListener(() =>
        //{
        //    ExitGame();
        //});

        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void Win()
    {
        CanvasGroup canvasGroup = m_WinPanel.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        m_IsWin = true;

        StartCoroutine(GoMainMenuCO());
    }

    public void GameOver()
    {
        CanvasGroup canvasGroup = m_GameOverPanel.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        m_IsGameOver = true;

        StartCoroutine(GoMainMenuCO());
    }

    //public void ExitGame()
    //{
    //    Application.Quit();
    //}

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        CanvasGroup canvasGroup = m_PausePanel.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        CanvasGroup canvasGroup = m_PausePanel.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator GoMainMenuCO()
    {
        float timer = 0;

        timer += Time.deltaTime;
        if(timer > 4)
        {
            yield return null;
        }

        GoMainMenu();

        
    }
}
