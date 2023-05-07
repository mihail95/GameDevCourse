using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    [SerializeField] GameObject placeholderTextGO;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameManager gameManager;
    private TMP_Text placeholderText;


    private void Start()
    {
        placeholderText = placeholderTextGO.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            if (inputField.text == "sudo rm -rf /*")
            {
                gameManager.SelectChoice(42);
                inputField.text = string.Empty;
                placeholderText.text = "Enter a valid shell command";
            }
            else
            {
                inputField.text = string.Empty;
                placeholderText.text = "Invalid Command, try again";
            }
        }
    }
}
