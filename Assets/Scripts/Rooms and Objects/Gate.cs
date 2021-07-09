using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : RoomObject
{
    private bool _isOpen;
    public bool IsOpen
    {
        get { return _isOpen; }
        set
        {
            _isOpen = value;
            // Open or close gate (ie enable/disable collider and change visuals)
        }
    }
}
