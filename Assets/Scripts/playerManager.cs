using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{
    public static bool g;
    public static bool gameOver;
    public GameObject gameOverPanel;
  //  public GameObject hs;

    public static bool isGameStarted;
    public GameObject startingText;
 


    public static int numberOfCoins;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI CoinHigh;

    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
        numberOfCoins = 0;
        CoinHigh.text = "Coin Highscore: " + PlayerPrefs.GetInt("maxcoin", 0).ToString();
    }

    void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
           
            Destroy(startingText);
        }

        coinText.text = "Coins: " + numberOfCoins;

        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText);
            
        }
        if (numberOfCoins > PlayerPrefs.GetInt("maxcoin", 0))
        {


            PlayerPrefs.SetInt("maxcoin", numberOfCoins);

            g = true;
            CoinHigh.text = "Coin Highscore : "+ numberOfCoins.ToString();
        }
        


    }
    public void reset()
    {

        PlayerPrefs.DeleteAll();

    }
}
