// Author: Bilal El Medkouri

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GiantAbilityEditor_Scrapped : EditorWindow
{
    public GiantAbilityList_Scrapped GiantAbilityList;
    private int viewIndex = 1;

    [MenuItem("Window/Giant Ability Editor %#e")]
    private static void Init()
    {
        GetWindow(typeof(GiantAbilityEditor_Scrapped));
    }

    private void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            GiantAbilityList = AssetDatabase.LoadAssetAtPath(objectPath, typeof(GiantAbilityList_Scrapped)) as GiantAbilityList_Scrapped;
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Giant Ability Editor", EditorStyles.boldLabel);
        if (GiantAbilityList != null)
        {
            if (GUILayout.Button("Show Ability List"))
            {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = GiantAbilityList;
            }
        }
        if (GUILayout.Button("Open Ability List"))
        {
            OpenAbilityList();
        }
        if (GUILayout.Button("New Ability List"))
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = GiantAbilityList;
        }
        GUILayout.EndHorizontal();

        if (GiantAbilityList == null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            if (GUILayout.Button("Create New Ability List", GUILayout.ExpandWidth(false)))
            {
                CreateNewAbilityList();
            }
            if (GUILayout.Button("Open Existing Ability List", GUILayout.ExpandWidth(false)))
            {
                OpenAbilityList();
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(20f);

        if (GiantAbilityList != null)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10f);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex > 1)
                    viewIndex--;
            }

            GUILayout.Space(5f);

            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
            {
                if (viewIndex < GiantAbilityList.GiantAbilities.Count)
                    viewIndex++;
            }

            GUILayout.Space(60f);

            if (GUILayout.Button("Add Ability", GUILayout.ExpandWidth(false)))
            {
                AddAbility();
            }
            if (GUILayout.Button("Delete Ability", GUILayout.ExpandWidth(false)))
            {
                DeleteAbility(viewIndex - 1);
            }
            GUILayout.EndHorizontal();

            if (GiantAbilityList.GiantAbilities.Count > 0)
            {
                GUILayout.BeginHorizontal();
                viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Ability", viewIndex, GUILayout.ExpandWidth(false)), 1, GiantAbilityList.GiantAbilities.Count);
                EditorGUILayout.LabelField("of   " + GiantAbilityList.GiantAbilities.Count.ToString() + "  abilities", "", GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GiantAbilityList.GiantAbilities[viewIndex - 1].AbilityName = EditorGUILayout.TextField("Ability Name", GiantAbilityList.GiantAbilities[viewIndex - 1].AbilityName as string);
                GiantAbilityList.GiantAbilities[viewIndex - 1].AbilityObject = EditorGUILayout.ObjectField("Ability Object", GiantAbilityList.GiantAbilities[viewIndex - 1].AbilityObject, typeof(GameObject), false) as GameObject;
                GiantAbilityList.GiantAbilities[viewIndex - 1].Damage = EditorGUILayout.IntField("Damage", GiantAbilityList.GiantAbilities[viewIndex - 1].Damage, GUILayout.ExpandWidth(false));
                GiantAbilityList.GiantAbilities[viewIndex - 1].Range = EditorGUILayout.FloatField("Range", GiantAbilityList.GiantAbilities[viewIndex - 1].Range, GUILayout.ExpandWidth(false));
                GiantAbilityList.GiantAbilities[viewIndex - 1].Cooldown = EditorGUILayout.FloatField("Cooldown", GiantAbilityList.GiantAbilities[viewIndex - 1].Cooldown, GUILayout.ExpandWidth(false));
                GiantAbilityList.GiantAbilities[viewIndex - 1].unlockedAtPhase = (GiantAbility_Scrapped.UnlockedAtPhase)EditorGUILayout.EnumPopup("Unlocked At Phase", GiantAbilityList.GiantAbilities[viewIndex - 1].unlockedAtPhase);

                GUILayout.Space(10f);
            }
            else
            {
                GUILayout.Label("This Ability List is Empty.");
            }
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(GiantAbilityList);
        }
    }

    private void CreateNewAbilityList()
    {
        viewIndex = 1;

        GiantAbilityList = CreateGiantAbilityList_Scrapped.Create();
        if (GiantAbilityList)
        {
            GiantAbilityList.GiantAbilities = new List<GiantAbility_Scrapped>();
            string relPath = AssetDatabase.GetAssetPath(GiantAbilityList);
            EditorPrefs.SetString("ObjectPath", relPath);
        }
    }

    private void OpenAbilityList()
    {
        string absPath = EditorUtility.OpenFilePanel("Select Ability Item List", "", "");
        if (absPath.StartsWith(Application.dataPath))
        {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            GiantAbilityList = AssetDatabase.LoadAssetAtPath(relPath, typeof(GiantAbilityList_Scrapped)) as GiantAbilityList_Scrapped;

            if (GiantAbilityList.GiantAbilities == null)
            {
                GiantAbilityList.GiantAbilities = new List<GiantAbility_Scrapped>();
            }
            if (GiantAbilityList)
            {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }

    private void AddAbility()
    {
        GiantAbility_Scrapped newGiantAbility = new GiantAbility_Scrapped { AbilityName = "New Giant Ability" };

        GiantAbilityList.GiantAbilities.Add(newGiantAbility);
        viewIndex = GiantAbilityList.GiantAbilities.Count;
    }

    private void DeleteAbility(int index)
    {
        GiantAbilityList.GiantAbilities.RemoveAt(index);
    }
}
