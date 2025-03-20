using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Start() {
        pauseMenuUI.SetActive(false); // Hide pause menu when game starts
    }


    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause(){
        if(!isPaused) {
            Pause();
        }
        else {
            Resume();
        }
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        Time.timeScale= 1f;

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops Play Mode in Unity Editor
        #else
        Application.Quit(); // Quits in a built game
        #endif
    }

}
