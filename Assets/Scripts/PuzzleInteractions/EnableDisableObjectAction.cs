using Assets.Scripts.Interaction;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/EnableDisableObject")]
public class EnableDisableObjectAction : InteractionData
{
    public GameObject objectToDisable;
    public GameObject objectToEnable;

    public override void Execute(InteractionContext ctx)
    {
        if (objectToDisable != null)
            objectToDisable.SetActive(false);

        if (objectToEnable != null)
            objectToEnable.SetActive(true);
    }
}