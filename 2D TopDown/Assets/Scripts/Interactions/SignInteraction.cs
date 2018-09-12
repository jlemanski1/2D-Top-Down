using UnityEngine;
using UnityEngine.UI;

public class SignInteraction : MonoBehaviour {

    [SerializeField] private GameObject signUI;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text signText;
    [SerializeField] private Text interactText;
    private BoxCollider2D sign;
    private GameObject player;

	void Start () {
        sign = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    /// <summary>
    /// Triggers the text box when the player is in range, and chooses to interact
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            signUI.SetActive(true);
            interactText.text = "Press E to interact";
            
            if (Input.GetAxis("Interact") >= 1) {
                interactText.text = "";
                panel.SetActive(true);
            }
        }
    }


    /// <summary>
    /// Closes the text box when the player strays from the sign
    /// </summary>
    private void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player") {
            panel.SetActive(false);
            signUI.SetActive(false);
        }
    }
}
