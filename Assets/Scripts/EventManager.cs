using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void statusAccion();
    public static event statusAccion OnGameOver;
    public static event statusAccion OnAsteroidDestroyed;

    private static EventManager instance;

    public void AsteroidDestroyed()
    {
        if (OnAsteroidDestroyed != null)
            OnAsteroidDestroyed();
    }
    
    public void GameOver()
    {
        if (OnGameOver != null)
            OnGameOver();
    }

    private EventManager()
    {

    }

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;
        }
    }
}
