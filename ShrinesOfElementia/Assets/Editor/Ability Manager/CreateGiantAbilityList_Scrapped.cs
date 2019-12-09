// Author: Bilal El Medkouri

using UnityEditor;
using UnityEngine;

public class CreateGiantAbilityList_Scrapped
{
    [MenuItem("Assets/Create/Giant Ability List")]
    public static GiantAbilityList_Scrapped Create()
    {
        GiantAbilityList_Scrapped asset = ScriptableObject.CreateInstance<GiantAbilityList_Scrapped>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/_Ability Manager/Giants/GiantAbilityList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
