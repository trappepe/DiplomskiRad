using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Core
{
    public class SceneBootstrap : MonoBehaviour
    {
        public Transform playerStart;
        public Transform cameraStart;

        public GameObject infoPanel;
        public TMP_Text infoText;
        public GameObject dialoguePanel;
        public TMP_Text dialogueText;
        public Button yesButton;
        public Button noButton;

        public GameObject puzzlePanel;
        public GameObject objectToEnable;
        public GameObject objectToDisable;

        void Awake()
        {
            GameManager.instance.SetScene(playerStart, cameraStart);
            UIManager.instance.SetObjects(infoPanel, infoText, dialoguePanel, dialogueText, yesButton, noButton, puzzlePanel);
        
            if (!GameManager.instance.worldChanged)
            {
                if (objectToEnable != null)
                    objectToEnable.SetActive(true);

                if (objectToDisable != null)
                    objectToDisable.SetActive(false);
            }
        }
    }
}
