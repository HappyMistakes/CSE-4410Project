
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class StartButon : MonoBehaviour
{
    public GameObject Options;
    public GameObject MainMenu;
    public void ChangeSceneStart()
    {
        SceneManager.LoadScene("DungeonLevel1");
    }

    public void ChangeSceneOptions()
    {
        //SceneManager.LoadScene("Settings");
        Options.gameObject.SetActive(true);
        MainMenu.gameObject.SetActive(false);
    }
    public void OptionsBack()
    {
        //SceneManager.LoadScene("Settings");
        Options.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    
    public void Quit()
    {
        Debug.Log("Game is exeting...");

        Application.Quit();
    }
        
}
