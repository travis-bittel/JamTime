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
        _isVisible = visibleVisionModes.Contains(GameManager.Instance.CurrentVisionMode);

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (col == null)
        {
            col = GetComponent<Collider2D>();
        }

        if (spriteRenderer != null && col != null)
        {
            if (_isVisible)
            {
                spriteRenderer.enabled = true;

                if (isOpen)
                {
                    spriteRenderer.sprite = openSprite;
                    col.enabled = false;
                }
                else
                {
                    spriteRenderer.sprite = closedSprite;
                    col.enabled = true;
                }
            } else
            {
                spriteRenderer.enabled = false;
                col.enabled = false;
            }
        }
    }
}
