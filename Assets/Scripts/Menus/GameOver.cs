using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject gameOverScreen;


    // Start is called before the first frame update
    void Start() {
        gameOverScreen.SetActive(false); // Hides on start    
    }

    public void ShowGameOver(){
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f; //Pauses the Game
    }

        public void RestartGame() {
        Time.timeScale= 1f; // Resume the Game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reload scene
    }

    public void QuitGame() {
        Debug.Log("Game Quit");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity Editor
        #else
        Application.Quit(); // Quits the game in a built version
        #endif
    }

}
