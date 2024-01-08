using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    public SpriteRenderer[] wallElements;
    float alphaValue=1f;
    public float disappearRate=1f;
    bool playerEntered=false;
    public bool toggleWall=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si el jugador entra a la zona secreta el alpha (transparencia) se resta a razon de la multiplicacion de Time.deltaTime y disappearRate
        if(playerEntered){
            alphaValue -=Time.deltaTime * disappearRate;
            if(alphaValue<=0)
                //Ya que no va a dar el valor justo al hacer la resta cuando se menor que 0 el alphaValue este se actualiza a 0
                alphaValue=0;
                //por cada objeto en el array se le actualiza el valor alpha
                foreach(SpriteRenderer wallItem in wallElements){
                    wallItem.color=new Color(wallItem.color.r, wallItem.color.g, wallItem.color.b, alphaValue);
                }
        }
        //Si el jugador sale de la zona el alpha se suma y se le añade este valor a cada objeto en el array
        else{
            alphaValue+=Time.deltaTime*disappearRate;
            if(alphaValue>=1)
            alphaValue=1;
            foreach(SpriteRenderer wallItem in wallElements){
                    wallItem.color=new Color(wallItem.color.r, wallItem.color.g, wallItem.color.b, alphaValue);
                }
        }
        
    }

    //Metodo para comprobar que el jugador entra a la zona
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            playerEntered=true;
        }
    }

    //Metodo para comprobar que el jugador sale de la zona
   private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") && toggleWall){
            playerEntered=false;
        }
    }
}
