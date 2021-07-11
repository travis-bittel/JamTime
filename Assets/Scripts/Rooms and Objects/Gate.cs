using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Gate : RoomObject
{
    public bool isOpen;

    [SerializeField]
    private Sprite closedSprite;
    [SerializeField]
    private Sprite openSprite;

    [FMODUnity.EventRef]
    public string open_sfx, close_sfx;

    public void SetOpenState(bool open)
    {
        FMODUnity.RuntimeManager.PlayOneShot(open ? open_sfx : close_sfx, transform.position);
        isOpen = open;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            if (isOpen)
            {
                spriteRenderer.sprite = openSprite;
            } else
            {
                spriteRenderer.sprite = closedSprite;
            }
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
