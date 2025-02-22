using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeButtonPos : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform joystick;
    public void OnPointerClick(PointerEventData eventData)
    {
        // Get the position of the click
        Vector2 clickPosition = eventData.position;

        // Print or use the click position
        Debug.Log("Clicked at position: " + clickPosition);

        joystick.position = clickPosition;
    }
}
