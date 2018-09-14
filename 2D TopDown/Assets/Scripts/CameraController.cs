using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles following the player and smoothing the camera
/// </summary>
public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 targetPos;

    [Header("Camera Settings")]
    [SerializeField]
    private Vector3 offset;     // Offset between camera center and player
    public float interpVelocity;
    

	void Awake () {
        targetPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	
	void FixedUpdate () {
        if (player) {
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 targetDirection = (player.transform.position - posNoZ);
            interpVelocity = targetDirection.magnitude * 5f;
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }
	}
}
