using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallContolPanal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action<Vector2> PointDownAction;
    
    public Action<Vector2> PointUpAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        PointDownAction.Invoke(eventData.position);        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointUpAction.Invoke(eventData.position);  
    }
}
