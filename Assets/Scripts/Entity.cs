using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	[Header("Entity")]
	public int healthPoints;
	public int damagePoints;

	public virtual void ReceiveDamage(int damagePoints)
	{
		healthPoints -= damagePoints;
		if(healthPoints <= 0)
		{
			DeActivate();
		}
	}

	public virtual void DeActivate()
	{
		gameObject.SetActive(false);
	}
}
