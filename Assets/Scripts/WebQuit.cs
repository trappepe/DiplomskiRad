using UnityEngine;

public class WebQuit : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            // Redirects the browser tab to a specific URL
            Application.OpenURL("https://yourwebsite.com");
        #else
            // Standard quit for PC/Mac/Linux standalone builds
            Application.Quit();
        #endif
    }
}