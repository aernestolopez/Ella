using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScenes : MonoBehaviour
{
    [SerializeField]Image enter;
    [SerializeField]Image cruz;
    public float targetTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
    enter.enabled=false;  
    cruz.enabled=false; 
    }

    // Update is called once per frame
    void Update()
    {
        //Se crea un contador de tiempo para que cuando pasen 10 segundos se active la ayuda para cambiar la escena
        if(targetTime>0.0f){
         targetTime -= Time.deltaTime;
         if(targetTime<0.0f)
         {
            enter.enabled=true;
            cruz.enabled=true;
         }
        }
    }

    public void Avanzar(InputAction.CallbackContext context){
        if(context.performed)
        {
            //Dependiendo de la escena donde se encuentre se cargara una escena u otra
            if(SceneManager.GetActiveScene().name=="Recompensa1")
            {
            SceneManager.LoadScene("Pico");
            }
            if(SceneManager.GetActiveScene().name=="Estadisticas")
            {
                SceneManager.LoadScene("MenuPrincipal");
            }
        }
    }
}
