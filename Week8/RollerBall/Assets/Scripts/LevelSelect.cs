using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] RawImage thisImage;

    public void ChangeHoverColor(bool isHovering)
    {
        thisImage.color = isHovering ? Color.gray : Color.white;
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("level1");
    }
}
