using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject pauseButton;

    public static bool isGameStarted;
    public GameObject startingText;

    public static int numberOfCoins;
    public static int totalCoins;
    public int bestCountCoins;

    public Text coinsText;
    public Text totalCoinsText;
    public Text bestCountCoinsText;

    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        Time.timeScale = 1;
        numberOfCoins = 0;
        totalCoins = 0;
        bestCountCoins = PlayerPrefs.GetInt("BestCountCoins", 0);
        bestCountCoinsText.text = "ксвьее йнкхвеярбн лнмер: " + bestCountCoins;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            pauseButton.SetActive(false);
            totalCoinsText.text = "хрнцнбне йнкхвеярбн лнмер: " + totalCoins;
            UpdateBestCountCoins();
        }
        coinsText.text = "Coins: " + numberOfCoins;
        

        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText); 
        }
    }

    void UpdateBestCountCoins()
    {
        if (totalCoins > bestCountCoins)
        {
            bestCountCoins = totalCoins;
            PlayerPrefs.SetInt("BestCountCoins", bestCountCoins);
        }
        bestCountCoinsText.text = "ксвьее йнкхвеярбн лнмер: " + bestCountCoins;
    }
}
