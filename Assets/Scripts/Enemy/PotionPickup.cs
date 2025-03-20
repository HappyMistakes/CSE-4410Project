using UnityEngine;

public class PotionPickup : MonoBehaviour {

private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("Player")) {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null) {
            player.AddPotion(1); // Add 1 potion to the player's inventory
            Destroy(gameObject); // Remove the potion from the scene
        }
    }
}

}
