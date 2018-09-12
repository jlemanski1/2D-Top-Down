using UnityEngine;
using UnityEngine.UI;

public class SignInteraction : MonoBehaviour {

    [SerializeField] private GameObject signUI;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text signText;
    [SerializeField] private Text interactText;
    private Collider2D sign;
    private bool inRange;

	void Start () {
        sign = GetComponent<Collider2D>();
        inRange = false;
    }


    /// <summary>
    /// Triggers the text box when the player is in range
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            inRange = true;
            signUI.SetActive(true);
            Debug.Log("Player within range");
            interactText.text = "Press E to interact"; 
        }
    }


    /// <summary>
    /// Opens the sign text box when the player interacts
    /// </summary>
    private void OnTriggerStay2D(Collider2D col) {
        if (inRange) {
            if (Input.GetKeyDown(KeyCode.E)) {
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
            inRange = false;
            panel.SetActive(false);
            signUI.SetActive(false);
        }
    }
}
