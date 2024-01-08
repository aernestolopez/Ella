using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;

public class Querys : MonoBehaviour
{
    string puntos="";
    string deaths="";

    public GameObject frutas;
    public GameObject muertes;
    string sceneName;
     Scene m_Scene;
    // Start is called before the first frame update
    void Start()
    {
        Query1("SELECT * from user");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Si el personaje colisiona con el tag BaseDatos se llama al metodo
    void OnTriggerEnter2D(Collider2D other)
    {
       //Se hace un update a la base de datos
        if(other.tag=="BaseDatos")
        {
            BaseDatos db=new BaseDatos();
            string fruits = frutas.GetComponent<TextMeshProUGUI>().text;
            string death = muertes.GetComponent<TextMeshProUGUI>().text;
            m_Scene = SceneManager.GetActiveScene();
            sceneName = m_Scene.name;
             db.Query("UPDATE usuario SET points = '"+fruits+"', deaths = '"+death+"', level = '"+sceneName+"' WHERE  id = 1;");
        }
    }

    //Metodo para leer datos de la base de datos
    public void Query1(string q)
    {
        using (var connection = new SqliteConnection("URI=file:DataBase.db"))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = q;
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Los datos leidos se mandan al TextMeshPro de Unity
                        puntos= (string)reader["points"];
                        deaths= (string)reader["deaths"];
                        frutas.GetComponent<TextMeshProUGUI>().text=puntos;
                        muertes.GetComponent<TextMeshProUGUI>().text=deaths;
                    }
                }
            }
            connection.Close();
        }
    }
}
