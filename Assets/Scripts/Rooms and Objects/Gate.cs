using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : RoomObject
{
    public bool isOpen;

    public void SetOpenState(bool open)
    {
        isOpen = open;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = !isOpen;
        }

        if (col == null)
        {
            col = GetComponent<Collider2D>();
        }
        if (col != null)
        {
            col.enabled = !isOpen;
        }
    }

    public override void UpdateVisibility()
    {
        _isVisible = !isOpen && visibleVisionModes.Contains(GameManager.Instance.CurrentVisionMode);

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
