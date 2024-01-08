 using UnityEngine;
 
 public class FPS : MonoBehaviour 
 {
    //FPS maximos
     public int targetFrameRate = 60;
 
     private void Start()
     {
         QualitySettings.vSyncCount = 0;
         //Los FPS para el juego seran 60 como maximo
         Application.targetFrameRate = targetFrameRate;
     }
 }
