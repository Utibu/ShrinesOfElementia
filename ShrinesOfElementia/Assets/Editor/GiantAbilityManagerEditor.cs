// Author: Bilal El Medkouri

using UnityEditor;

[CustomEditor(typeof(GiantAbilityManager))]
public class GiantAbilityManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GiantAbilityManager myGiantAbilityManager = (GiantAbilityManager)target;


    }
}
