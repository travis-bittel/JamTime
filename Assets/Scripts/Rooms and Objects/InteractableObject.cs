using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for anything the player can interact with by pressing the Interact Key.
public abstract class InteractableObject : RoomObject
{
    public int interactionPriority;

    public abstract void OnInteract();
}
