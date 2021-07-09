using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Buttons that can be interacted with by the player.
public class ButtonObject : InteractableObject
{
    // Specifies what the button does
    public List<ButtonObjectInteraction> buttonInteractions;

    public void OnInteract()
    {
        foreach (ButtonObjectInteraction interaction in buttonInteractions)
        {
            switch (interaction.actionType)
            {
                case ButtonActionType.OPEN_GATE:
                    if (interaction.roomObject is Gate)
                    {
                        ((Gate)interaction.roomObject).IsOpen = true;
                    } else
                    {
                        Debug.LogError("Attempted ButtonActionType OPEN_GATE on non-Gate object");
                    }
                    break;
                case ButtonActionType.CLOSE_GATE:
                    if (interaction.roomObject is Gate)
                    {
                        ((Gate)interaction.roomObject).IsOpen = false;
                    }
                    else
                    {
                        Debug.LogError("Attempted ButtonActionType CLOSE_GATE on non-Gate object");
                    }
                    break;
                case ButtonActionType.TOGGLE_GATE:
                    if (interaction.roomObject is Gate)
                    {
                        ((Gate)interaction.roomObject).IsOpen = !((Gate)interaction.roomObject).IsOpen;
                    }
                    else
                    {
                        Debug.LogError("Attempted ButtonActionType TOGGLE_GATE on non-Gate object");
                    }
                    break;
            }
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
