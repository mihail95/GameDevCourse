using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public TMP_Text startText;

    public void ChangeTextColor(bool hasEntered)
    {
        startText.color = hasEntered ? Color.grey : Color.white;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("GameScreen");
    }
}
