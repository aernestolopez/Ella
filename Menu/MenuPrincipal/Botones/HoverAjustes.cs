using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverAjustes : MonoBehaviour,  IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<Animator>().SetBool("hover", true);
        
    }

    public void OnPointerExit(PointerEventData eventData){
         eventData.pointerEnter.GetComponent<Animator>().SetBool("hover", false);
        
    }
}
