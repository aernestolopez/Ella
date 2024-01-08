using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    private void Awake()
    {
        //No pueden haber mas de un GameEventsManager
        if (instance != null)
        {
            Debug.LogError("Se encontraron mas de un GameEventsManager en la escena.");
        }
        instance = this;
    }
    //Se crea el evento de muerte del personaje
    public event Action onPlayerDeath;
    public void PlayerDeath() 
    {
        if (onPlayerDeath != null) 
        {
            onPlayerDeath();
            
        }
    }
    //Se crea el eventro de Fruta recogida
    public event Action onFruitCollected;
    public void FruitCollected() 
    {
        if (onFruitCollected != null) 
        {
            onFruitCollected();
        }
    }
}