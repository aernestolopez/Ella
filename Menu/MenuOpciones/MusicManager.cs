using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    // Start is called before the first frame update
    void Start()
    {
        //Si existe un PlayerPrefs de volumen se carga el volumen
        if(PlayerPrefs.HasKey("Volumen"))
        {
            CargarVolumen();
        }
        //Si no se llama al metodo para cambiar volumen
        else
        {
            CambiarVolumen();
        }
        
    }

    //Metodo para cambiar volumen
    public void CambiarVolumen()
    {
        //Segun el valor del slider se cambia el volumen mediante el audioMixer
        float volumen=musicSlider.value;
        audioMixer.SetFloat("Volumen", volumen);
        PlayerPrefs.SetFloat("Volumen", volumen);
    }

    //Metodo para cargar el volumen
    private void CargarVolumen()
    {
        //El valor del slider sera el que este guardado en el playerPrefs
        musicSlider.value=PlayerPrefs.GetFloat("Volumen");
        //Se llama al metodo de cambiar volumen para cambiar el audioMixer
        CambiarVolumen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
