using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpaceShip : Entity {

	[Header("SpaceShip")]
	public GameObject[] health;
	public Vector2 direction;
	public Rigidbody2D rBody;
	public float horizontalSpeed;
	public float verticalSpeed;
	public float axis;
	public bool isGyroActivated;
	public bool hasPiercingShot;
	
	private void Awake() {
		isGyroActivated = PlayerPrefs.GetInt("Gyro") == 1 ? true : false;

		direction = transform.position;
		rBody = GetComponent<Rigidbody2D>();
		for (int i = 0; i < healthPoints; i++)
			health[i] = GameObject.Find("IHealth" + i);
	}

	private void FixedUpdate() {
		#if UNITY_ANDROID
			if(isGyroActivated)
				rBody.velocity = new Vector2(Input.acceleration.x * horizontalSpeed, verticalSpeed);
			else
				rBody.velocity = new Vector2(axis * horizontalSpeed, verticalSpeed);
		#endif
		#if UNITY_EDITOR
			rBody.velocity = new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed, verticalSpeed);
		#endif

		direction.y += verticalSpeed;
	}

	public void Shot()
	{
		var bullet = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.BULLET);
		bullet.transform.position = transform.position;
		var bulletScript = bullet.GetComponent<Bullet>();
		bulletScript.isPiercingShot = hasPiercingShot;
		bulletScript.bulletSpeed = verticalSpeed + 200f;
		bulletScript.damagePoints = damagePoints;
		bullet.SetActive(true);
	}

	public override void ReceiveDamage(int damagePoints)
	{
		health[healthPoints - 1].SetActive(false);
		healthPoints--;
		damagePoints--;
		
		if(damagePoints == 0)
			damagePoints = 1;
		
		hasPiercingShot = false;

		if(healthPoints <= 0)
			DeActivate();
	}

	public override void DeActivate()
	{
		EventManager.Instance.GameOver();
		Destroy(gameObject);
	}
}
