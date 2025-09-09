using UnityEditor;
using UnityEngine;

public class MenuDuplicator : EditorWindow
{
    
    private SerializedObject serializedObject;
    private Vector2 scrollPosition = Vector2.zero;
    public MenuData MenuData;
    public int Counter;
    [MenuItem("Debug Tools/Tools/Menu Duplicator")]
    public static void DisplayWindow()
    {
        var _window = GetWindow(typeof(MenuDuplicator), false, "Menu Duplicator");
        _window.minSize = new Vector2(500, 700);
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
    }

    private void OnGUI()
    {
        serializedObject.Update();
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, 
            GUIStyle.none, GUI.skin.verticalScrollbar);

        DisplayDuplicatorTools();
            
        GUILayout.EndScrollView();
        serializedObject.ApplyModifiedProperties();
    }
    private void DisplayDuplicatorTools()
    {
        EditorGUILayout.BeginVertical(new GUIStyle("GroupBox"));
        EditorGUILayout.LabelField("Duplictor Tool");
        MenuData = (MenuData)EditorGUILayout.ObjectField(
            "Menu Data",MenuData, typeof(MenuData), false);
        Counter = EditorGUILayout.IntField("Amount", Counter);

        if (GUILayout.Button("Duplicate"))
        {
            duplicateMenu(MenuData, Counter);
        }
        EditorGUILayout.EndVertical();
    }

    private void duplicateMenu(MenuData  data,int count)
    {

        for (int i = 0; i < count; i++)
        {
            var itemData = data.itemData;
            var newItemData = ScriptableObject.CreateInstance<ItemData>();
            var itempath = AssetDatabase.GetAssetPath(itemData);
            var newItemPath=itempath.Replace(".asset", "") + $"{i+2}.asset";
            var menupath = AssetDatabase.GetAssetPath( data);
            var newMenuPath =menupath.Replace(".asset", "") + $"{i+2}.asset";
            var oriMenu = AssetDatabase.LoadAssetAtPath<MenuData>(itempath);
            if (oriMenu != null)
            {
                continue;
            }
            if (!AssetDatabase.CopyAsset(itempath, newItemPath))
            {
                continue;
            }
            
            if (!AssetDatabase.CopyAsset(menupath, newMenuPath))
            {
                continue;
            }
            var newItem =  AssetDatabase.LoadAssetAtPath<ItemData >(newItemPath);
            var newMenu =  AssetDatabase.LoadAssetAtPath<MenuData >(newMenuPath);
            newMenu.setItemData( newItem);
            newItem .setNumber($"{i+2}");
            EditorUtility.SetDirty( newMenu);
            EditorUtility.SetDirty(newItem);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
