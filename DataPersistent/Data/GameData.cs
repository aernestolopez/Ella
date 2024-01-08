using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class GameData
{
 public int deathCount;
 public Vector3 playerPosition;
 public string sceneName;
 public SerializableDictionary<string, bool> fruitsCollected;

 public Vector3 respawnPoint;

   //Los valores de este constructor serán los valores predeterminados
   //cuando el juego comience y no haya datos cargados
 public GameData()
 {
    this.deathCount = 0;
    playerPosition=Vector3.zero;
    fruitsCollected=new SerializableDictionary<string, bool>();
    this.sceneName="";
    this.respawnPoint=Vector3.zero;
 }
}
