using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("Seguir a")]
    [SerializeField] private Transform targetTransform;

    [Header("Configuracion")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private Vector2 offset = Vector2.zero;

    //Posicion inicial
    private Transform originalTargetTransform;

    private void Start() 
    {
        originalTargetTransform = targetTransform;
    }

    private void LateUpdate() 
    {
        //Si no se indica a que debe seguir no se mueve
        
        if (targetTransform == null) 
        {
            return;
        }

        float newPosX = this.transform.position.x;
        float newPosY = this.transform.position.y;
        //Se actualiza la posicion X
        if (followX) 
        {
            newPosX = targetTransform.position.x + offset.x;
        }
        //Se actualiza la posicion Y
        if (followY) 
        {
            newPosY = targetTransform.position.y + offset.y;
        }
        //Se actauliza la posicion de la camara
        this.transform.position = new Vector3(newPosX, newPosY, this.transform.position.z);
    }

}