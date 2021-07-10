using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public string text;
    public Color color;

    public bool isFixedText;
    public string[] fixedTextParagraph;

    private void Start()
    {
        if (!isFixedText && fixedTextParagraph.Length != 0)
        {
            Debug.LogWarning("TextTrigger on object named " + gameObject.name + " has fixedText provided, but is not set to show fixedText!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (color.a == 0)
        {
            color = Color.white;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isFixedText)
            {
                if (fixedTextParagraph.Length == 0)
                {
                    Debug.LogWarning("TextTrigger on object named " + gameObject.name + " was set to fixedText, but no fixedText was provided!");
                }
                else
                {
                    TextManager.Instance.DisplayFixedText(color, fixedTextParagraph);
                }
            }
            else
            {
                TextManager.Instance.DisplayFloatingText(text, color);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TextManager.Instance.DisplayFloatingText("", null);
        }
    }
}
