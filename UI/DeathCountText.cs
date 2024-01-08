using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathCountText : MonoBehaviour, IDataPersistence
{
    private int deathCount = 0;


    private TextMeshProUGUI deathCountText;

    private void Awake() 
    {
        //Obtenemos el componente del Texto
        deathCountText = this.GetComponent<TextMeshProUGUI>();
    }

    public void LoadData(GameData data)
    {
        //Cargamos los datos de las muertes   
        this.deathCount = data.deathCount;
    }

    public void SaveData(ref GameData data)
    {
        //Guardamos los datos de las muertes
        data.deathCount = this.deathCount;

    }

    private void Start() 
    {
        //Nos suscribimos al evento cuando comienza la escena
        GameEventsManager.instance.onPlayerDeath += OnPlayerDeath;
    }

    private void OnDestroy() 
    {
        //Nos desuscribimos al evento cuando se destruye
        GameEventsManager.instance.onPlayerDeath -= OnPlayerDeath;
    }

    //Metodo para aumentar el contador de muertes
    private void OnPlayerDeath() 
    {
        deathCount++;
    }


    private void Update() 
    {
        //Se actualiza el contador de muertes por cada frame
        deathCountText.text = "" + deathCount;
    }
}