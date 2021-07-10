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

    public virtual void UpdateVisibility()
    {
        _isVisible = visibleVisionModes.Contains(GameManager.Instance.CurrentVisionMode);

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
