using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public SpaceShip player;
	public float changePoint;

	private void Awake() {
		player = GameObject.Find("Player").GetComponent<SpaceShip>();
	}
	public void OnPointerDown(PointerEventData eventData){
    	player.axis -= changePoint;
	}
 
	public void OnPointerUp(PointerEventData eventData){
		player.axis += changePoint;
	}
}
