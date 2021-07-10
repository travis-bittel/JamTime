using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public string text;
    public Color color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (color.a == 0)
        {
            color = Color.white;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            TextManager.Instance.DisplayFloatingText(text, color);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TextManager.Instance.DisplayFloatingText("", null);
    }
}
