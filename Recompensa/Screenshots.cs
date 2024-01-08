using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting;
public class Screenshots : MonoBehaviour
{
    void Start(){
        TakeScreenshot();
    }



    public void TakeScreenshot()
    {
        //Se crea el directorio para la recompensa
        string directoryName = "Recompensa";
        DirectoryInfo screenshotDirectory = Directory.CreateDirectory(directoryName);
        //Se guarda la fecha
        string timeNow = DateTime.Now.ToString("dd-MMMM-yyyy HHmmss");
        //Se pone nombre a la imagen
        string fileName = ("recompensa-" + timeNow + ".png");
        //Se combina la ruta
        string fullPath = Path.Combine(screenshotDirectory.FullName, fileName);
        //Hacemos una captura de pantalla de la escena y se guarda en la ruta pasada
        ScreenCapture.CaptureScreenshot(fullPath);

    }
}