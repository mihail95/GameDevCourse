using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [SerializeField] Sprite yourOffice;
    [SerializeField] Sprite monitorCloseUp;
    [SerializeField] Sprite accountsClosed;
    [SerializeField] Sprite accountsOpen;
    [SerializeField] Sprite bossOffice;
    [SerializeField] Sprite meetingRoom;
    [SerializeField] Sprite breakRoom;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ChangeImage(string imageName)
    {
        Color color = spriteRenderer.color;
        switch (imageName)
        {
            case "YourOffice":
                color.a = 100;
                spriteRenderer.color = color;
                spriteRenderer.sprite = yourOffice;
                break;

            case "Monitor":
                color.a = 100;
                spriteRenderer.color = color;
                spriteRenderer.sprite = monitorCloseUp;
                break;

            case "AccountsClosed":
                color.a = 100;
                spriteRenderer.color = color;
                spriteRenderer.sprite = accountsClosed;
                break;

            case "AccountsOpen":
                color.a = 100;
                spriteRenderer.color = color;
                spriteRenderer.sprite = accountsOpen;
                break;

            case "BossOffice":
                color.a = 100;
                spriteRenderer.color = color;
                spriteRenderer.sprite = bossOffice;
                break;

            case "MeetingRoom":
                color.a = 100;
                spriteRenderer.color = color;
                spriteRenderer.sprite = meetingRoom;
                break;

            case "BreakRoom":
                color.a = 100;
                spriteRenderer.color = color;
                spriteRenderer.sprite = breakRoom;
                break;

            default:
                color.a = 0;
                spriteRenderer.color = color;
                break;
        }
        
    }
}
