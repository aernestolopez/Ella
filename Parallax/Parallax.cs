using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

public Transform camara;
public float movimiento=0.3f;
public bool bloquearY=false;

    // Update is called once per frame
    void Update()
    {
        //Si se bloquea la Y el fondo solo se movera en el eje X siguiendo a la camara segun el movimiento que se le da
        if(bloquearY){
            transform.position=new Vector2(camara.position.x*movimiento, transform.position.y);
        //Si no se mueve en ambos ejes siguiendo a la camara
        }else{
            transform.position=new Vector2(camara.position.x * movimiento, camara.position.y * movimiento);
        }

    }
}
