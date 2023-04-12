using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button resetColor;
    private Player player;

    public void ResetColors()
    {
        try
        {
            player = GameManager.GetCurrentPlayer();
            player.ResetSpriteColors();
        }
        catch { Debug.Log("No Player found!"); }
    }
}