using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform player;
	public float delay;
	public float offsetY;
	public float offsetX;
	public bool isActive;
	public int count;

	private void Start() {
		player = GameObject.Find("Player").transform;
		StartCoroutine(Spawn());
	}

	public IEnumerator Spawn()
	{
		while(isActive && player)
		{
			int enemyToSpawn = Random.Range(0, 4);
			GameObject asteroid;
			switch(enemyToSpawn)
			{
				case 0:
					asteroid = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.ASTEROID1);
				break;

				case 1:
					asteroid = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.ASTEROID2);
				break;

				case 2:
					asteroid = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.ASTEROID3);
				break;

				case 3:
					asteroid = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.ASTEROID4);
				break;

				default:
					asteroid = PoolManager.instance.GetInactivePrefab((int) Constants.IDS.ASTEROID1);
				break;
			}
			asteroid.transform.position = new Vector3(
				Random.Range(-offsetX, offsetX),
				player.transform.position.y + offsetY,
				player.position.z);
			asteroid.SetActive(true);

			yield return new WaitForSeconds(delay + Random.Range(0f, .25f));
		}
	}
}
