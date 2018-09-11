using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Main Class for Portals: Doors, entranceways, etc.
/// Transitions to the next scene using a loading screen with async operations.
/// </summary>

[RequireComponent(typeof(Collider2D))]
public class PortalController : MonoBehaviour {

    public string nextScene;    // Name of next scene to change to
    private Collider2D portal;
    private const float LOAD_READY_PERCENTAGE = 0.9f;   // Actual percentage when scene is fully loaded

    AsyncOperation sceneLoad;
    [SerializeField] GameObject loadingUI;
    [SerializeField] Slider loadingBar;
    [SerializeField] Text loadingText;

	
	void Start () {
        portal = GetComponent<Collider2D>();
	}


    /// <summary>
    /// Updates Loading Text & Progress bar, and starts the loading coroutine
    /// </summary>
    /// <param name="sceneName">Name of scene to load</param>
    public void ChangeScene(string sceneName) {
        loadingUI.SetActive(true);
        loadingText.text = "L O A D I N G . . .";
        StartCoroutine(LoadingSceneRealProgress(sceneName));
    }


    /// <summary>
    /// Loads new scene in async operation, updates text and progress bar
    /// </summary>
    IEnumerator LoadingSceneRealProgress(string sceneName) {
        yield return new WaitForSeconds(1);
        sceneLoad = SceneManager.LoadSceneAsync(sceneName);

        // Disable scene activation while loading to prevent auto loading
        sceneLoad.allowSceneActivation = false;

        while (!sceneLoad.isDone) {
            loadingBar.value = sceneLoad.progress;

            if (sceneLoad.progress >= LOAD_READY_PERCENTAGE) {
                loadingBar.value = 1f;
                loadingText.text = "PRESS SPACE TO CONTINUE";

                // User input to continue
                if (Input.GetKeyDown(KeyCode.Space))
                    sceneLoad.allowSceneActivation = true;
            }
            Debug.Log(sceneLoad.progress);
            yield return null;
        }
    }


    /// <summary>
    /// Portal entered by player
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            ChangeScene(nextScene);
            Debug.Log(portal.name + " portal entered!");
        }
        
    }


}
