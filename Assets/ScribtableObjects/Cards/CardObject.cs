using UnityEngine;
using Modifiers;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Card", order = 1)]
public class CardObject : ScriptableObject
{
    public string Name;
    public Texture2D Icon;
    public string[] ModifierNames;

    public IModifier[] Modifiers { get; private set; }

    void Awake()
    {
        Modifiers = new IModifier[ModifierNames.Length];
        for(int i = 0; i < ModifierNames.Length; i++)
        {
            Modifiers[i] = StringModifierMap.CreateModifierFromName(ModifierNames[i]);
        }
    }
}
