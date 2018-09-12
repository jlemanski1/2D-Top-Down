using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// Main Class for Portals: Doors, entranceways, etc.
/// Transitions to the next scene using a loading screen with async operations.
/// </summary>

[RequireComponent(typeof(Collider2D))]
public class PortalController : MonoBehaviour {

    private const float LOAD_READY_PERCENTAGE = 0.9f;   // Actual percentage when scene is fully loaded

    [Header("Portal Settings")]
    public string nextScene;        // Name of next scene to change to
    public Vector3 spawnOffset;     // Offset for player to spawn leaving portal
    private Collider2D portal;

    private GameObject player;

    AsyncOperation sceneLoad;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private Text loadingText;

	
	void Start () {
        portal = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
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
                if (Input.GetKeyDown(KeyCode.Space)) {
                    sceneLoad.allowSceneActivation = true;
                    SetPlayerPosition(spawnOffset);
                }
            }
            Debug.Log(sceneLoad.progress);
            yield return null;
        }
    }


    /// <summary>
    /// Sets the player's position to a set offset of the portal collider
    /// </summary>
    /// <param name="offset">Offset to spawn player from portal</param>
    private void SetPlayerPosition(Vector3 offset) {
        player.transform.SetPositionAndRotation(portal.transform.position + offset, player.transform.rotation);
        Debug.Log("Portal Pos: " + portal.transform.position + "\nPlayer Pos: " +  player.transform.position);
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
