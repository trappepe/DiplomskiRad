using UnityEngine;

[CreateAssetMenu(menuName = "NPC/NPC Data")]
public class NPCData : ScriptableObject
{
    [Header("Animation")]
    public string talkTrigger;

    [Header("Dialogue")]
    public string noItemText;
    public string hasItemText;
    public string finishedText;
    public string normalText;

    [Header("Conditions")]
    public bool requiresItem = true;
    public bool blocksAfterFinish = true;
}