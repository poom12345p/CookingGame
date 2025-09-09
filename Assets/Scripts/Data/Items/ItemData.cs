using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField]public string Key { get; private set; }= "";
    [field: SerializeField]public string Name { get; private set; }= "";
    [field: SerializeField]public Sprite Sprite { get; private set; } = null;

    public void setNumber(string number)
    {
        this.Key +=number;
        this.Name +=number;
        
    }
}
