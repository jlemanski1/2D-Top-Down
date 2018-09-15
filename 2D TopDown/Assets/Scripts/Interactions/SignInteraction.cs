using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// </summary>
public class SignInteraction : MonoBehaviour {

    [SerializeField] private GameObject signUI;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text signText;
    [SerializeField] private Text interactText;
    [TextArea] public string signMessage;
    private bool inRange;       // Player in range of sign


	private void Start () {
        inRange = false;
        signText.text = signMessage;
    }


    /// <summary>
    /// Triggers the text box when the player is in range
    /// </summary>
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            inRange = true;
            // TODO: Make wait 1 sec before setting active
            //       So it pops same time as pressing E works (OnTriggerStay2D is called)
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
