using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    
   
    void Start()
    {
    }
    public GameObject MenuIncial;
    public GameObject Menuopcion;
    public void salir(){
        MenuIncial.SetActive(true);
        Menuopcion.SetActive(false);
    }
}
