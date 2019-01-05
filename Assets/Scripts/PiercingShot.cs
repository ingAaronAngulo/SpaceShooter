using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingShot : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals(Constants.TAG_PLAYER))
		{
			other.transform.parent.GetComponent<SpaceShip>().hasPiercingShot = true;
			gameObject.SetActive(false);
		}
	}
}
