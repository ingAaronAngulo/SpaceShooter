using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Entity {

	[Header("Asteroid")]
	public float rotationSpeed;
	public Vector3 rotationDirection;
	public bool isSelfDestroyed;
	public Vector2 forceDirection;
	public float horizontalSpeed;
	public Rigidbody2D rBody;
	public int initialHealth;

	private void Awake() {
		initialHealth = healthPoints;
		rBody = GetComponent<Rigidbody2D>();
		rotationDirection = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)) * Time.deltaTime * rotationSpeed;
		forceDirection = new Vector2(Random.Range(-horizontalSpeed, horizontalSpeed),0);
	}

	private void FixedUpdate() {
		transform.Rotate(rotationDirection);
		rBody.AddForce(forceDirection);
	}

	public override void DeActivate()
	{
		if(!isSelfDestroyed)
		{
			for (int i = 0; i < initialHealth; i++)
				EventManager.Instance.AsteroidDestroyed();
			
			int randomNumber = Random.Range(0, 100);
			GameObject powerUp = null;
			switch(randomNumber)
			{
				case 0:
					powerUp = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.PIERCING_SHOT);
				break;

				case 1: 
					powerUp = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.ATTACK_UP);
				break;
			}
			if(powerUp)
			{
				powerUp.transform.position = transform.position;
				powerUp.SetActive(true);
			}
		}
				
		isSelfDestroyed = false;
		healthPoints = initialHealth;
		base.DeActivate();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag.Equals(Constants.TAG_PLAYER))
		{
			other.transform.parent.GetComponent<Entity>().ReceiveDamage(damagePoints);
			isSelfDestroyed = true;
			DeActivate();
		}
	}
}
