using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public static PoolManager instance;
    [Header("Entities")]
    public List<GameObject> prefabs;
    public List<GameObject> poolAsteroid1;
    public List<GameObject> poolAsteroid2;
    public List<GameObject> poolAsteroid3;
    public List<GameObject> poolAsteroid4;
    public List<GameObject> poolBullet;
    public List<GameObject> poolPiercingShot;
    public List<GameObject> poolAttackUp;

	private void Awake() {
		if (instance == null)
            instance = this;
        else
            if (instance != this)
                Destroy(gameObject);
		
		for (int i = 0; i < poolAsteroid1.Count; i++)
            poolAsteroid1[i] = CreatePrefab((int) Constants.IDS.ASTEROID1);
		for (int i = 0; i < poolAsteroid2.Count; i++)
            poolAsteroid2[i] = CreatePrefab((int) Constants.IDS.ASTEROID2);
		for (int i = 0; i < poolAsteroid3.Count; i++)
            poolAsteroid3[i] = CreatePrefab((int) Constants.IDS.ASTEROID3);
		for (int i = 0; i < poolAsteroid4.Count; i++)
            poolAsteroid4[i] = CreatePrefab((int) Constants.IDS.ASTEROID4);
        for (int i = 0; i < poolBullet.Count; i++)
            poolBullet[i] = CreatePrefab((int) Constants.IDS.BULLET);
        for (int i = 0; i < poolPiercingShot.Count; i++)
            poolPiercingShot[i] = CreatePrefab((int) Constants.IDS.PIERCING_SHOT);
        for (int i = 0; i < poolAttackUp.Count; i++)
            poolAttackUp[i] = CreatePrefab((int) Constants.IDS.ATTACK_UP);
	}

	public GameObject CreatePrefab(int module)
    {
        int realDigits = int.Parse(module.ToString().Substring(1, 2));
        GameObject entity = Instantiate(prefabs[realDigits]) as GameObject;
        entity.SetActive(false);
        return entity;
    }

    public GameObject GetInactivePrefab(int id)
    {
        List<GameObject> tempList = null;
        
        switch (id)
        {
            case (int) Constants.IDS.ASTEROID1:
                tempList = poolAsteroid1;
                break;

            case (int) Constants.IDS.ASTEROID2:
                tempList = poolAsteroid2;
                break;

            case (int) Constants.IDS.ASTEROID3:
                tempList = poolAsteroid3;
                break;
            
            case (int) Constants.IDS.ASTEROID4:
                tempList = poolAsteroid4;
                break;

            case (int) Constants.IDS.BULLET:
                tempList = poolBullet;
                break;

            case (int) Constants.IDS.ATTACK_UP:
                tempList = poolAttackUp;
                break;

            case (int) Constants.IDS.PIERCING_SHOT:
                tempList = poolPiercingShot;
                break;
            
            default:
                return null;
        }

        for (int i = 0; i < tempList.Count; i++)
            if (!tempList[i].activeInHierarchy)
                return tempList[i];

        tempList.Add(CreatePrefab(id));
        
        return tempList[tempList.Count - 1];
    }
}
