using UnityEngine;

public class InteractableObjects : MonoBehaviour, IInteract
{
    public InteractionData interaction;
    public Vector3 targetPosition;     
    public Vector3 targetRotation;     
     public GameObject objectToDisable;
    public GameObject objectToEnable;
    

    private bool playerInside = false;

    void Update()
    {
        if (!playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }
    public void Interact()
    {
        if (interaction == null)
            return;

        var ctx = GameManager.instance.GetContext(this.gameObject, targetPosition, targetRotation);
        
        ctx.customDisable = objectToDisable;
        ctx.customEnable = objectToEnable;

        interaction.Execute(ctx);
        UIManager.instance.HideInfo();
    }

    public string GetPrompt()
    {
        return interaction != null ? interaction.Prompt : "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        UIManager.instance.ShowInfo($"{GetPrompt()}");
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
        UIManager.instance.HideInfo();
    }
}
