using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public GameObject Instructionpanel;
   
   public void Start()
    {
        AdmobAds.Instance.ShowBanner();

    }
    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void Instruction()
    {
        Instructionpanel.SetActive(true);
       
        AdmobAds.Instance.ShowBanner();

    }
    public void exitinstructionpanel()
    {
        Instructionpanel.SetActive(false);
       

    }

}
