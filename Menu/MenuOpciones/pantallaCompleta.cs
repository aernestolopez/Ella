using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
using TMPro;
//

public class pantallaCompleta : MonoBehaviour
{
    public Toggle toggle;

    //
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;
    //

    void Start()
    {
        //Si esta en pantalla completa el toggle estara en ON si no estara apagado
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        //
        RevisarResolucion();
        //
    }


    void Update()
    {

    }

    public void ActiveFULLS(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;

    }

    //
    public void RevisarResolucion()
    {
        //Guardamos las resoluciones
        resoluciones = Screen.resolutions;
        //Limpiamos las opciones del dropdown
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;
        //Guardamos en la lista las opciones
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height +" " + resoluciones[i].refreshRate + "Hz";
            opciones.Add(opcion);

            //Si en el for damos con la resolucion con la que se encuentra la pantalla ahora mismo se marcara como activa en el dropdown
            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }

        }
        //Añadimos las opciones
        resolucionesDropDown.AddOptions(opciones);
        //Añadimos el valor
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();
        //Guardamos en las playerprefs el valor actual
        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        //Al cambiar de resolucion actualizamos el playerpref
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);

        Resolution resolution = resoluciones[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    //
}