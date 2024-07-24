#if UNITY_EDITOR
/*
using UnityEditor;

[CustomEditor(typeof(CommonAttackAction))]
public class CommonAttackActionCustomInspector : ActionCustomInspector
{
    private bool TimeOrientedFoldOut = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        CommonAttackAction owner = (CommonAttackAction)target;

        owner.animationOriented = EditorGUILayout.Toggle("AnimationOriented", owner.animationOriented);
        if (!owner.animationOriented)
        {
            TimeOrientedFoldOut = EditorGUILayout.Foldout(TimeOrientedFoldOut, "TimeOriented");
            if (TimeOrientedFoldOut)
            {
                owner.activatingHitBoxTime = EditorGUILayout.FloatField("ActivatingHitBoxTime", owner.activatingHitBoxTime);
                owner.cancelableTime = EditorGUILayout.FloatField("CancelableTime", owner.cancelableTime);
            }
        }
        
    }
}
*/
#endif