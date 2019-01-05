using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals(Constants.TAG_ENEMY) || other.tag.Equals(Constants.TAG_BULLET))
		{
			
			var asteroid = other.GetComponent<Asteroid>();
			if(asteroid)
			{
				asteroid.isSelfDestroyed = true;
				asteroid.DeActivate();
				return;
			}

			var entity = other.GetComponent<Entity>();
			if(entity)
				entity.DeActivate();
		}
	}
}
