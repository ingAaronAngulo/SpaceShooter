using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Transform player;
	public Vector3 objectivePosition;
	public float offsetY;

	private void Awake() {
		player = GameObject.Find("Player").transform;
		objectivePosition = player.position;
		objectivePosition.z -= 50;
	}

	private void FixedUpdate() {
		if(player)
		{
			objectivePosition.y = player.position.y + offsetY;
			transform.position = objectivePosition;
		}
	}
}
