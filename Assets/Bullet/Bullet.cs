using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity {

	public float bulletSpeed;
	public Vector2 forceDirection;
	public Rigidbody2D rBody;
	public bool isPiercingShot;

	private void OnEnable() {
		forceDirection = new Vector2(0, bulletSpeed);
	}
	private void Awake() {
		rBody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() {
		rBody.velocity = forceDirection;
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals(Constants.TAG_ENEMY))
		{
			other.GetComponent<Entity>().ReceiveDamage(damagePoints);
			if(!isPiercingShot)
				DeActivate();
		}
	}
}
