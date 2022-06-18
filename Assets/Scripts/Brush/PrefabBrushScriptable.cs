#if (UNITY_EDITOR) 
using UnityEditor.Tilemaps;
using UnityEngine;

/**
 * @link https://www.youtube.com/watch?v=_wDT_MIv3sM
 */

[CreateAssetMenu(fileName = "Prefab Brush", menuName = "Brushes/Prefab Brush")]
[CustomGridBrush(false, true, false, "Prefab Brush")]
public class PrefabBrushScriptable : GameObjectBrush
{
}
#endif