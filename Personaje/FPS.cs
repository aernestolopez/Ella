 using UnityEngine;
 
 public class FPS : MonoBehaviour 
 {
    //FPS maximos
     public int targetFrameRate = 144;
 
     private void Start()
     {
         QualitySettings.vSyncCount = 0;
         //Los FPS para el juego seran 144 como maximo
         Application.targetFrameRate = targetFrameRate;
     }
 }
