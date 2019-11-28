// Author: Bilal El Medkouri

using UnityEditor;
using UnityEngine;

public class MenuItems
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearPlayerPrefsMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Elemental Studios/Shrines of Elementia")]
    private static void NewElementalStudiosMenuOption()
    {

    }
}
