using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using JetBrains.Annotations;

public class Events : MonoBehaviour
{

    public GameObject resume;
    public static bool sound=true;

    public void ReplayGame()
    {
       
        SceneManager.LoadScene("game");
    }
    public void QuitGame()
    {
        
        SceneManager.LoadScene("main menu");
        AdmobAds.Instance.ShowInterstitial();
    }
   public void pause ()
    {
        Time.timeScale = 0;
        resume.SetActive(true);

    }
    public void resumeplay()
    {
        Time.timeScale = 1;
        resume.SetActive(false);

    }
    public void reset()
    {

        PlayerPrefs.DeleteAll();

    }
   






    



}
