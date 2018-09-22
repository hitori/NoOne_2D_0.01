using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemTemplate))]
public class ItemTemplateCustomEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemTemplate item = (ItemTemplate)target;

        //EditorGUILayout.BeginHorizontal();
        //if (GUILayout.Button("Consumable"))
        //{

        //    //EditorGUILayout.BeginHorizontal();
        //    if (GUILayout.Button("Food"))
        //    {

        //    }

        //    if (GUILayout.Button("Drinks"))
        //    {

        //    }

        //    if (GUILayout.Button("EnergyBar"))
        //    {

        //    }
        //    //EditorGUILayout.EndHorizontal();
        //}

        //if (GUILayout.Button("Equipment"))
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    if (GUILayout.Button("Feet"))
        //    {

        //    }

        //    if (GUILayout.Button("Legs"))
        //    {

        //    }

        //    if (GUILayout.Button("LegsProtection"))
        //    {

        //    }

        //    if (GUILayout.Button("Belt"))
        //    {

        //    }

        //    if (GUILayout.Button("Body"))
        //    {

        //    }

        //    if (GUILayout.Button("BodyProtection"))
        //    {

        //    }

        //    if (GUILayout.Button("Gloves"))
        //    {

        //    }

        //    if (GUILayout.Button("Head"))
        //    {

        //    }

        //    if (GUILayout.Button("Weapon"))
        //    {

        //    }
        //    EditorGUILayout.EndHorizontal();
        //}

        //if (GUILayout.Button("Ammo"))
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    if (GUILayout.Button("Handgun ammo"))
        //    {

        //    }

        //    if (GUILayout.Button("AssaultRifle ammo"))
        //    {

        //    }

        //    if (GUILayout.Button("SniperRifle ammo"))
        //    {

        //    }

        //    if (GUILayout.Button("Shotgun ammo"))
        //    {

        //    }
        //    EditorGUILayout.EndHorizontal();
        //}

        //if (GUILayout.Button("Keys"))
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    if (GUILayout.Button("Ordibary key"))
        //    {

        //    }

        //    if (GUILayout.Button("CardKey"))
        //    {

        //    }

        //    if (GUILayout.Button("TabletKey"))
        //    {

        //    }

        //    if (GUILayout.Button("Small Key"))
        //    {

        //    }

        //    if (GUILayout.Button("Lockpick"))
        //    {

        //    }
        //    EditorGUILayout.EndHorizontal();
        //}

        //if (GUILayout.Button("Trading"))
        //{
        //    EditorGUILayout.BeginHorizontal();
            
        //    EditorGUILayout.EndHorizontal();
        //}
        //EditorGUILayout.EndHorizontal();

        //if (GUILayout.Button("MeleeShort"))
        //{

        //}

        //if (GUILayout.Button("MeleeLong"))
        //{

        //}

        //if (GUILayout.Button("FirearmsPistol"))
        //{

        //}

        //if (GUILayout.Button("FirearmsAssaultRifle"))
        //{

        //}

    }

}
