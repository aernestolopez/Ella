using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitCollected : MonoBehaviour, IDataPersistence
{

    private int fruitsCollected = 0;

    private TextMeshProUGUI fruitsCollectedText;

    private void Awake() 
    {
        //Obtenemos el componente del Texto
        fruitsCollectedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
       //Nos suscribimos al evento
        GameEventsManager.instance.onFruitCollected += OnFruitCollected;
    }

    public void LoadData(GameData data)
    {
        //Cargamos los datos de las frutas
        foreach(KeyValuePair<string, bool> pair in data.fruitsCollected)
        {
            if(pair.Value)
            {
                fruitsCollected++;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        //No hay datos para guardar en este script
    }

    private void OnDestroy() 
    {
        // Nos desuscribimos del evento
        GameEventsManager.instance.onFruitCollected -= OnFruitCollected;
    }

    //Metodo para aumentar el contador de frutas
    private void OnFruitCollected() 
    {
        fruitsCollected++;
    }

    private void Update() 
    {
        //El contador de frutas se actualiza cada frame
        fruitsCollectedText.text = ""+fruitsCollected;
    }
}