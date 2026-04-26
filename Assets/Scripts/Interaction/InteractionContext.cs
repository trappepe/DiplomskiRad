using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class InteractionContext
    {
        public GameObject player;
        public GameObject target;
        public Vector3 focusTransform;
        public Vector3 focusRotation;

        public UIManager ui;

        public PlayerController playerController;
        public PlayerCamera playerCamera;

        public GameManager gameManager;
        public GameObject customDisable;
        public GameObject customEnable;
    }
}
