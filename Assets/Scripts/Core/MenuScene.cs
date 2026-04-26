using UnityEngine; 

public class MainMenu : MonoBehaviour 
{
    public GameObject player;
    public GameObject cameraHolder;
    void Start()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(cameraHolder);

        GameManager.instance.RegisterPlayer(player, cameraHolder);
    }

    public void NewGame()
    {
        StartCoroutine(GameManager.instance.SceneLoading(1));
    }

    public void Exit()
    {
        GameManager.instance.Exit();
    }
}