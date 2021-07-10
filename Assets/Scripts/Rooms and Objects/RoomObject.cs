using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for objects that appear in a room. Is inherited by more specific classes for each object type.
public class RoomObject : MonoBehaviour
{
    public List<VisionMode> visibleVisionModes; // List of all vision modes that the object is visible within
    protected bool _isVisible;

    protected SpriteRenderer spriteRenderer;

    protected Collider2D col;

    public bool IsVisible
    {
        get { return _isVisible; }
    }

    public void UpdateVisibility()
    {
        if (visibleVisionModes.Contains(GameManager.Instance.CurrentVisionMode))
        {
            _isVisible = true;
        } else
        {
            _isVisible = false;
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = _isVisible;
        }

        if (col == null)
        {
            col = GetComponent<Collider2D>();
        }
        if (col != null)
        {
            col.enabled = _isVisible;
        }
    }
}
