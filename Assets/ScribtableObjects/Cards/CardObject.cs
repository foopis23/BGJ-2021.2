using System;
using UnityEngine;
using Modifiers;

[CreateAssetMenu(fileName = "New Card", menuName = "ScriptableObjects/Card", order = 1)]
public class CardObject : ScriptableObject
{
    public string Name;
    public Texture2D Icon;
    public string[] ModifierNames;

    public IModifier[] Modifiers { get; private set; }
    public float ChaosLevel { get; set; }

    public string FlavorText { get; private set; }

    public void Init()
    {
        ChaosLevel = 0f;
        Modifiers = new IModifier[ModifierNames.Length];
        FlavorText = "";
        for(int i = 0; i < ModifierNames.Length; i++)
        {
            string[] modifierData = ModifierNames[i].Split(' ');
            Modifiers[i] = StringModifierMap.CreateModifierFromName(modifierData[0],  Int32.Parse(modifierData[1]));
            Modifiers[i].SetCard(this);
            FlavorText += $"{Modifiers[i].GetFlavorText()}\n";
        }
    }
}
