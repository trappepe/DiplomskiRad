using UnityEngine;

public class LightsOutRaycast : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit: " + hit.transform.name);
                LightTile tile = hit.transform.GetComponent<LightTile>();

                if (tile != null)
                {
                    tile.ToggleWithNeighbors();
                    CheckWin();
                }
            }
            Debug.Log("Nothing hit");
        }

{
    

    
}
    }

    void CheckWin()
    {
        LightTile[] allTiles = FindObjectsOfType<LightTile>();

        foreach (var t in allTiles)
        {
            if (!t.isOn) return; // 🔥 win only if ALL are ON
        }

        Debug.Log("PUZZLE SOLVED!");
        GameManager.instance.UnlockMovementAndCamera();
    }
}
