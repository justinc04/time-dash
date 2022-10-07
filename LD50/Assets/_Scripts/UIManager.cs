using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Image fade;

    public GameObject playerUI;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text scoreLabel;
    public Text finalScoreText;
    public Text playAgainText;
    public Text homeText;

    public Text timeText;
    public Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fade.enabled = true;
        fade.DOFade(0f, 1f).OnComplete(() => Faded());
    }

    void Faded()
    {
        fade.enabled = false;
        GameManager.Instance.StartGame();
    }

    private void Update()
    {
        timeText.text = ((int)GameManager.Instance.time + 1).ToString();

        if (GameManager.Instance.time <= 5)
        {
            timeText.color = Color.red;
        }
        else
        {
            timeText.color = Color.black;
        }

        scoreText.text = "Score: " + GameManager.Instance.score;
    }

    public void GameOver()
    {
        playerUI.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = GameManager.Instance.score.ToString();
        gameOverPanel.GetComponent<Image>().DOFade(.8f, .5f).OnComplete(() => FadeTexts());
    }

    void FadeTexts()
    {
        gameOverText.DOFade(1f, 1f);
        scoreLabel.DOFade(1f, 1f);
        finalScoreText.DOFade(1f, 1f);
        playAgainText.DOFade(1f, 1f);
        homeText.DOFade(1f, 1f);
    }

    public void OnClickPlayAgain()
    {
        fade.enabled = true;
        fade.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void OnClickHome()
    {
        fade.enabled = true;
        fade.DOFade(1f, 1f).OnComplete(() => SceneManager.LoadScene(0));
    }
}
