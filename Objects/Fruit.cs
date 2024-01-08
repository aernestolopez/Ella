using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour, IDataPersistence
{
    //Generar ID unicos para el diccionario
    [SerializeField] private string id;

    [ContextMenu("Generar ID")]

    private void GenerateGuid()
    {
        id=System.Guid.NewGuid().ToString();
    }
    private SpriteRenderer visual;
    private bool collected = false;

    private void Awake() 
    {
        //Obtenemos el sprite
        visual = this.GetComponentInChildren<SpriteRenderer>();
    }

    public void LoadData(GameData data)
    {
        //Se intenta obtener el id de la fruta sin importar si ha sido o no recogida
        data.fruitsCollected.TryGetValue(id, out collected);
        if(collected)
        {
            //Desactivamos el objeto
            visual.gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        //Si ya existe lo borramos antes de guardarlo de nuevo
        if(data.fruitsCollected.ContainsKey(id))
        {
            data.fruitsCollected.Remove(id);
        }
        data.fruitsCollected.Add(id, collected);
    }

    //Colision de la fruta con el jugador
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Player")
        {
        //Si no esta recogida se llama al metodo para recogerla
        if (!collected) 
        {
            CollectFruit();
        }
        }
    }

    //Metodo que recoge una fruta
    private void CollectFruit() 
    {
        //Se pone a true collected
        collected = true;
        //Se desactiva la imagen de la fruta
        visual.gameObject.SetActive(false);
        //Se llama al evento de FruitCollected
        GameEventsManager.instance.FruitCollected();
    }

}