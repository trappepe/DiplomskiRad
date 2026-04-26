#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.Interaction;
using UnityEngine.UI;
using Assets.Scripts.Core;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Systems")]
    public UIManager ui;
    public FocusCamera focusCamera;

    [Header("State")]
    public GameObject uiPrefab;
    private GameObject uiInstance;
    public bool puzzleFinished;
    public bool finishedFrame;
    public Image itemUIIcon;
    public Sprite drinkIcon;
    public Sprite keyIcon;
    public ItemTypes currentItem = ItemTypes.None;
    public bool worldChanged = false;


    private GameObject player;
    private GameObject mainCamera;
    private PlayerController playerController;
    private PlayerCamera playerCamera;
    private InteractionContext interactionContext;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        if (uiInstance == null)
        {
            uiInstance = Instantiate(uiPrefab);
            DontDestroyOnLoad(uiInstance);

            itemUIIcon = uiInstance.GetComponentInChildren<Image>();

            itemUIIcon.color = new Color(1, 1, 1, 0);
        }
    }

    public void RegisterPlayer(GameObject p, GameObject cam)
    {
        player = p;
        playerController = p.GetComponent<PlayerController>();
        mainCamera = cam;
        playerCamera = cam.GetComponent<PlayerCamera>();
        Transform camTarget = player.transform.Find("CameraPosition");
        playerCamera.target = camTarget != null ? camTarget : player.transform;
    }

    public void SetScene(Transform playerStart, Transform cameraStart)
    {
        player.SetActive(false);
        mainCamera.SetActive(false);
        player.transform.position = playerStart.position;
        player.transform.rotation = playerStart.rotation;

        playerCamera.transform.position = cameraStart.position;
        playerCamera.transform.rotation = cameraStart.rotation;

        playerCamera.ResetRotations();

        player.SetActive(true);
        mainCamera.SetActive(true);
    }

    public InteractionContext GetContext(GameObject target, Vector3 transform, Vector3 rotation)
    {
        if (interactionContext == null)
            interactionContext = new InteractionContext();

        interactionContext.player = player;
        interactionContext.target = target;
        if (transform != null)
            interactionContext.focusTransform = transform;
        if (rotation != null)
            interactionContext.focusRotation = rotation;
        interactionContext.ui = ui;
        //interactionContext.camera = focusCamera;
        interactionContext.playerController = playerController;
        interactionContext.playerCamera = playerCamera;
        interactionContext.gameManager = this;

        return interactionContext;
    }

    public IEnumerator SceneLoading(int index)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        yield return new WaitUntil(() => scene.progress == 0.9f);

        scene.allowSceneActivation = true;

        yield return new WaitUntil(() => scene.isDone);

        GameManager.instance.UnlockMovementAndCamera();
    }
    public void LoadSceneWithDelay(int index, float delay)
{
    StartCoroutine(Delayed(index, delay));
}

private IEnumerator Delayed(int index, float delay)
{
    yield return new WaitForSeconds(delay);
    StartCoroutine(SceneLoading(index));
}

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public PlayerController GetPlayerController()
    { 
        return playerController; 
    }

    public PlayerCamera GetPlayerCamera()
    {
        return playerCamera;
    }

    public void LockMovementAndCamera()
    {
        if (playerController != null)
            playerController.enabled = false;

        if (playerCamera)
            playerCamera.enabled = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
       // RemoveItemVisual();
    }

    public void UnlockMovementAndCamera()
    {
        if (playerController)
            playerController.enabled = true;

        if (playerCamera)
            playerCamera.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void NextLevel(int level)
    {
        StartCoroutine(SceneLoading(level));
    }

    public void GiveItem(ItemTypes item)
    {
        currentItem = item;
        if (itemUIIcon == null)
            return;
        SetItemIconVisible(true);
        switch (item)
        {
            case ItemTypes.Drink:
            itemUIIcon.sprite = drinkIcon;
            break;
            case ItemTypes.Keys:
            itemUIIcon.sprite = keyIcon;
            break;
            default:
            
            SetItemIconVisible(false);
            break;
        }
    }
    public bool HasItem(ItemTypes item)
    { 
        return currentItem == item;
    }
    public void ConsumeItem()
    {
        currentItem = ItemTypes.None;
        SetItemIconVisible(false);
    }
    public void RemoveItemVisual()
    {
        itemUIIcon.color = new Color(1, 1, 1, 0);
    }
    void SetItemIconVisible(bool visible)
{
    if (itemUIIcon == null) return;

    Color c = itemUIIcon.color;
    c.a = visible ? 1f : 0f;
    itemUIIcon.color = c;
}
}
