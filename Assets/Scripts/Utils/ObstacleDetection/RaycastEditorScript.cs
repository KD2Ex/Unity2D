using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(RaycastDetection))]
public class RaycastEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        RaycastDetection raycastDetection = (RaycastDetection) target;

        if (GUILayout.Button("Cast"))
        { 
        }
        
        if (GUILayout.Button("Compute Retreat"))
        {
            raycastDetection.GetRetreatDirection();
        }
        
    }
}
#endif
