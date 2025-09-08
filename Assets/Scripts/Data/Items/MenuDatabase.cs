using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuDataBase", menuName = "ScriptableObject/MenuDataBase")]
public class MenuDatabase : ScriptableObject
{
    [field: SerializeField] public List<MenuData> Menus {get; private set; } =new List<MenuData>();
}
