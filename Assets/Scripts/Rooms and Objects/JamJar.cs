using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A jar of jam that the player can pick up and carry.
public class JamJar : InteractableObject
{
    public VisionMode type;

    public override void OnInteract()
    {
        Player.Instance.heldJamColor = type;
        GameManager.Instance.CurrentVisionMode = VisionMode.DEFAULT;
        // Play pickup sound
    }

    private void Start()
    {
        // Set sprite to correct one for the type
    }
}
