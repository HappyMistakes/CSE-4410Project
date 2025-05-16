using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour {
    public GameObject victoryScreen; // Assign the VictoryScreen panel in the Inspector

    void Start() {
        victoryScreen.SetActive(false); // Hide Victory Screen at the start
    }

    public void ShowVictoryScreen() {
        victoryScreen.SetActive(true);  // Show the Victory Screen panel
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartLevel() {
        Time.timeScale = 1f; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload level
    }

    public void NextLevel() {
        Time.timeScale = 1f; // Resume game
        //Debug.Log("Next Level Placeholder! No next level set.");
        SceneManager.LoadScene("Level2"); // Uncomment when i have a next level
    }

    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity Editor
        #else
        Application.Quit(); // Quits the game in a built version
        #endif
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) { // Check if Player touches the ExitDoor
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
                Debug.Log("All enemies defeated! You win!");
                ShowVictoryScreen(); // Show the victory screen
            } else {
                Debug.Log("Defeat all enemies first!");
            }
        }
    }
}
