using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button m_Start;
    public Button m_Quit;


    private void Awake()
    {
        m_Start = GameObject.Find("Button_Start").GetComponent<Button>();
        m_Quit  = GameObject.Find("Button_Quit").GetComponent<Button>();
    }

    private void Start()
    {
        m_Start.onClick.AddListener(() =>
        {

            StartGame();

        });


        m_Quit.onClick.AddListener(() =>
        {

            ExitGame();

        });
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
