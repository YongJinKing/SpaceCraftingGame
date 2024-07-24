#if UNITY_EDITOR
/*
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Action))]
public class ActionCustomInspector : Editor
{
    private bool AIToggle;
    public override void OnInspectorGUI()
    {
        Action owner = (Action)target;

        AIToggle = EditorGUILayout.Foldout(AIToggle, "For AI");
        if (AIToggle)
        {
            owner.priority = EditorGUILayout.FloatField("Priority", owner.priority);
            owner.activeRadius = EditorGUILayout.FloatField("ActiveRadius", owner.activeRadius);
        }
    }
}
*/
#endif