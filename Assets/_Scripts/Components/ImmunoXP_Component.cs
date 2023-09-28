using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImmunoXP_Component
{
    [System.Serializable]
    private class ImmunoXP //Classe que define o tipo de sistema Imune
    {
#if UNITY_EDITOR
        [HideInInspector]
        public string name;
#endif

        public ImmunoType immunoType; //Enum do tipo Imune
        [System.NonSerialized] public int currentValue = 0; //Valor Atual
        public int maxValue = 20; //Valor Máximo de XP
        public LevelUpModifiers levelUpFactor = new LevelUpModifiers(1.5f,1.5f); //Fator para LevelUpDaSkills
        public int level = 1; //O nível da XP
        [System.NonSerialized] public float immunoLevelDamageModifier = 1;
        public ImmunoXP(ImmunoType ImmunoType, int MaxValue = 20, int Level = 1, float LevelUpFactor = 1.5f) //Construtor da classe
        {
            immunoType = ImmunoType;
            maxValue = MaxValue;
            level = Level;
        }
    }
    [SerializeField] private ImmunoXP[] immunoXps = 
        {
        new ImmunoXP(ImmunoType.Virus), new ImmunoXP(ImmunoType.Bacteria), new ImmunoXP(ImmunoType.Fungi), new ImmunoXP(ImmunoType.Cancer), new ImmunoXP(ImmunoType.Parasyte) 
        }; //Os sistemas imunes específicos

    public void SetImmunoXPValue(ImmunoType immuno, int value)
    {
        foreach(ImmunoXP i in immunoXps)
        {
            if(i.immunoType == immuno)
            {
                i.currentValue += value;
                if(i.currentValue >= i.maxValue)
                {
                    UpgradeImmunoSystem(i);
                }
            }
        }
    }
    private void UpgradeImmunoSystem(ImmunoXP immuno)
    {
        immuno.level++;
        immuno.currentValue = immuno.currentValue - immuno.maxValue;
        immuno.maxValue *= (int)immuno.levelUpFactor.levelFactor;
        immuno.immunoLevelDamageModifier *= immuno.levelUpFactor.immunoFactor;
    }
    public float GetImmunoMultiplier(ImmunoType typeOfImmuneSystem)
    {
        foreach (ImmunoXP i in immunoXps)
        {
            if (i.immunoType == typeOfImmuneSystem)
            {
                return i.immunoLevelDamageModifier;
            }
        }
        return 0;
    }
}

[System.Serializable]
struct LevelUpModifiers
{
    public float levelFactor;
    public float immunoFactor;
    public LevelUpModifiers(float LevelFactor, float ImmunoFactor)
    {
        levelFactor = LevelFactor;
        immunoFactor = ImmunoFactor;
    }
}
public enum ImmunoType
{
    Virus,
    Bacteria,
    Fungi,
    Cancer,
    Parasyte
}
