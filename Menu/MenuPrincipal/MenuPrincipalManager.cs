using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class MenuPrincipalManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MenuOpciones;
    public GameObject MenuInicial;
    public string nombre="";
    FileDataHandler fh;
    GameObject comenzar;
    
    void Start()
    {
        //Creamos el objeto FileDataHandler
        fh=new FileDataHandler(Application.persistentDataPath, "save.json", true);
        comenzar=GameObject.FindGameObjectWithTag("Cargar");
        if(File.Exists(Path.Combine(Application.persistentDataPath, "save.json")))
        {
            comenzar.SetActive(true);
        }
        else
        {
            comenzar.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Metodo para comenzar la partida
    public void BotonComenzar()
    {
        //Se crea una nueva partida
        DataPersistenceManager.instance.NewGame();
       //Se crea un objeto de la base de datos para crear la tabla si no existe
        BaseDatos db=new BaseDatos();
        db.CreateTable();
        //Se borran los datos de la tabla por si existiese ya una tabla con el mismo nombre pero con datos
        db.Delete();
        //Se crean unos datos
        db.Query("INSERT INTO user (id, points, deaths, level) VALUES ('1','0', '0', '');");
        //Se guarda la partida
        DataPersistenceManager.instance.SaveGame();
        //Se carga el primer nivel
        SceneManager.LoadScene("Base");
    }

    //Metodo para cargar la partida
    public void BotonCargar()
    {
        //Si no hay datos guardados no se puede cargar una partida
        //if(fh.Load()==null)
        //{
        //    Debug.Log("No hay datos");
        //}
        //Si existe una partida se lee el archivo de guardado y se carga el nombre de la escena
        
            SceneManager.LoadScene(fh.Load().sceneName);
        
        
    }

    //Metodo para acceder a los ajustes
    public void BotonAjustes()
    {
        //Se desactiva este menu y se activa el de ajustes
        MenuInicial.SetActive(false);
        MenuOpciones.SetActive(true);
    }

    //Metodo para salir del juego
    public void BotonSalir()
    {
        Application.Quit();
    }

}