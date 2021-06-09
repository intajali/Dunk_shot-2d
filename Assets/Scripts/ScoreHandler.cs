using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    public static ScoreHandler instance;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private PauseScreenView pauseScreenView;
     public SettingScreenView settingScreenView;

    private int score = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        replayButton.onClick.RemoveAllListeners();
        replayButton.onClick.AddListener(OnClickReplay);

        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(OnClickPause);
    }

    private void Start()
    {
        replayButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Invoke("OnDisableLoadingScreen",3f);
    }

    private void OnDisableLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }

    public void OnClickReplay()
    {
        score = 0;
        replayButton.gameObject.SetActive(false);
        gameOverText.text = "";
        scoreText.text = "0";
        GameManager.instance.OnReplay();
        pauseButton.gameObject.SetActive(true);
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = score.ToString();
        GameManager.instance.UpdateGameOverLine();
    }

    public void OnGameOver()
    {
        gameOverText.text = "GAME OVER!";
        replayButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    private void OnClickPause()
    {
        pauseScreenView.Render();
    }


}
