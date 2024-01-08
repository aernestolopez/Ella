using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJuego : MonoBehaviour
{
    public GameObject MenuOpciones;
    public GameObject MenuInicial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotonContinuar()
    {
        //Se desactiva el menu de pausa y la escala de tiempo vuelve a ser 1
        MenuInicial.SetActive(false);
        Time.timeScale=1f;
    }

    public void BotonSalir()
    {
        //Se pone la escala del tiempo a 1 de nuevo y se carga la escena del menu principal
        Time.timeScale=1f;
        SceneManager.LoadScene(0);

    }

    public void BotonAjustes()
    {
        //Se activa el menu de opciones
        MenuOpciones.SetActive(true);
    }

    public void BotonGuardar()
    {   //Se pone la escala de tiempo a 1 para poder guardar los datos
        Time.timeScale=1f;
        //Se guardan los datos
        DataPersistenceManager.instance.SaveGame();
        //La escala se vuelve a pausar
        Time.timeScale=0f;
    }
}
