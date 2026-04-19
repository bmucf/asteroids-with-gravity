using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGameSession()
    {
        GameManager.instance.SwitchScenes("Testing");
    }

    public void Quit()
    {
        GameManager.instance.QuitGame();
    }
}
