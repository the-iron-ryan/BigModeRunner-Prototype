using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "New Prefab Brush", menuName = "Brushes/Prefab Brush")]
[CustomGridBrush(false, true, false, "Prefab Brush")]
public class PrefabBrush : GameObjectBrush
{
    public GameObject prefab;

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (prefab != null)
        {
            GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(prefab);
            if (instance != null)
            {
                Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint Prefabs");
                Undo.RegisterCreatedObjectUndo((Object) instance, "Paint Prefabs");
                instance.transform.SetParent(brushTarget.transform);
                instance.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position + new Vector3(.5f, .5f, .5f)));
            }
        }
    }

    public override void Erase(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget != null)
        {
            Transform erased = GetObjectInCell(gridLayout, brushTarget.transform, position);
            if (erased != null)
                Undo.DestroyObjectImmediate(erased.gameObject);
        }
    }

    private static Transform GetObjectInCell(GridLayout gridLayout, Transform parent, Vector3Int position)
    {
        int childCount = parent.childCount;
        Vector3 min = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position));
        Vector3 max = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position + Vector3Int.one));
        Bounds bounds = new Bounds((max + min) * .5f, max - min);
        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (bounds.Contains(child.position))
                return child;
        }
        return null;
    }
}