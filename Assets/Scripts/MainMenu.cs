using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string firstLevelName = "Level 1";

    public void PlayGame(bool isTwoPlayers)
    {
        PlayerSpawner.IsTwoPlayers = isTwoPlayers;
        SceneManager.LoadScene(firstLevelName);
    }
}
