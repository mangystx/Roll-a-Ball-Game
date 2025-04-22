using UnityEngine;
using UnityEngine.UI;

public class StartupManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject player;

    public static bool isGameStarted = false;
    private static bool hasStartedBefore = false;

    private void Start()
    {
        if (hasStartedBefore)
        {
            startMenu.SetActive(false);
            player.SetActive(true);
            isGameStarted = true;
        }
        else
        {
            startMenu.SetActive(true);
            player.SetActive(false);
            isGameStarted = false;
        }
    }

    public void StartGame()
    {
        hasStartedBefore = true;
        startMenu.SetActive(false);
        player.SetActive(true);
        isGameStarted = true;
    }
}