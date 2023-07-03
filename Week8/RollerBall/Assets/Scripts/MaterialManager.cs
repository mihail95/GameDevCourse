using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] RawImage volcanoBallIcon;
    [SerializeField] RawImage grassBallIcon;
    public static MaterialManager instance;
    private static bool volcanoBallSelected;
    private Color activeColor;
    private Color inactiveColor;
    public static MaterialManager GetInstance()
    {
        if (instance == null) { Debug.LogWarning("No MaterialManager Instance available!"); }
        return instance;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        volcanoBallSelected = true;
        activeColor = volcanoBallIcon.color;
        inactiveColor = grassBallIcon.color;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            instance.volcanoBallIcon.gameObject.SetActive(true);
            instance.grassBallIcon.gameObject.SetActive(true);
            if (instance.volcanoBallIcon == null || instance.grassBallIcon == null)
            {
                instance.volcanoBallIcon = GameObject.Find("VolcanoBall").GetComponent<RawImage>();
                instance.grassBallIcon = GameObject.Find("GrassBall").GetComponent<RawImage>();
            }
        }
        else
        {
            instance.volcanoBallIcon.gameObject.SetActive(false);
            instance.grassBallIcon.gameObject.SetActive(false);
        }
    }
    public void SelectBallMaterial(string name)
    {
        volcanoBallSelected = name switch
        {
            "volcano" => true,
            "grass" => false,
            _ => true,
        };

        volcanoBallIcon.color = volcanoBallSelected ? activeColor : inactiveColor;
        grassBallIcon.color = !volcanoBallSelected ? activeColor : inactiveColor;
    }

    public static bool IsDefautSkinSelected()
    {
        return volcanoBallSelected;
    }
}
