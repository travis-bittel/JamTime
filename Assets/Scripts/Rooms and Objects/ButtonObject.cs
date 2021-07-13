using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Buttons that can be interacted with by the player.
public class ButtonObject : InteractableObject
{
    // Specifies what the button does
    public List<ButtonObjectInteraction> buttonInteractions;

    public override void OnInteract()
    {
        int index = 0;
        foreach (ButtonObjectInteraction interaction in buttonInteractions)
        {
            switch (interaction.actionType)
            {
                case ButtonActionType.OPEN_GATE:
                    if (interaction.roomObject is Gate)
                    {
                        ((Gate)interaction.roomObject).SetOpenState(true);
                    } else
                    {
                        Debug.LogError("Attempted ButtonActionType OPEN_GATE on non-Gate object");
                    }
                    break;
                case ButtonActionType.CLOSE_GATE:
                    if (interaction.roomObject is Gate)
                    {
                        ((Gate)interaction.roomObject).SetOpenState(false);
                    }
                    else
                    {
                        Debug.LogError("Attempted ButtonActionType CLOSE_GATE on non-Gate object");
                    }
                    break;
                case ButtonActionType.TOGGLE_GATE:
                    if (interaction.roomObject is Gate)
                    {
                        ((Gate)interaction.roomObject).SetOpenState(!((Gate)interaction.roomObject).isOpen);
                    }
                    else
                    {
                        Debug.LogError("Attempted ButtonActionType TOGGLE_GATE on non-Gate object");
                    }
                    break;
            }
            // Set the line if this is the first gate interacted with, otherwise add it to the lineRenderer
            if (index == 0)
            {
                Player.Instance.DrawLineBetweenPlayerAndLocation(interaction.roomObject.transform.position);
            } else
            {
                Player.Instance.AddPositionToExistingLine(interaction.roomObject.transform.position);
            }
            index++;
        }
    }
}

// Each button has a list of these that determines what it does
[System.Serializable]
public struct ButtonObjectInteraction
{
    public RoomObject roomObject;
    public ButtonActionType actionType;
}

public enum ButtonActionType
{
    OPEN_GATE,
    CLOSE_GATE,
    TOGGLE_GATE
}
