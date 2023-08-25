using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour, IDamageable
{
    [System.Serializable]
    private class Stat //Classe que define os Stats do Jogo
    {
        [SerializeField] private string name; //Nome do Stat, não vai ser mudado durante o jogo!
        [SerializeField] private int baseValue; //Valor base do Stat para cada Objeto, ele define o valor Máximo e o Atual(O Atual só segue o valor base na Inicialização)
        [System.NonSerialized] public int currentValue; //Valor Atual
        [System.NonSerialized] protected int maxValue; //Valor Máximo
        public Stat(string StatName, int BaseValue) //Construtor dos Stats
        {
            name = StatName;
            baseValue = BaseValue;
            maxValue = baseValue;
            currentValue = baseValue;
        }
        public string GetName()
        {
            return name;
        }
    }

    //Declaração de Essenciais como padrão
    [SerializeField] private Stat[] stats = { new Stat("Vida", 20), new Stat("Estamina", 20), new Stat("Mana", 10)}; //Os Stats do Jogo, por padrão todos os scripts contém 3

#if UNITY_EDITOR
    [SerializeField] private bool DarDano;
    [SerializeField] private int Dano;
#endif

    public void TakeDamage(int inDamage, bool ignoreArmor = false) //Método da Interface IDamageable
    {
        Stat tempStat = FindDesiredStat("Vida");
        tempStat.currentValue -= inDamage;
        Debug.Log("Vida atual: " + tempStat.currentValue);
    }
    private Stat FindDesiredStat(string statName) //Método que retorna o Stat baseado no seu nome, caso inválido retorna o primeiro Stat e Printa no Console Erro
    {
        foreach(Stat currentStat in stats)
        {
            if(statName == currentStat.GetName())
            {
                return currentStat;
            }
        }
        Debug.LogError("INVALID STAT");
        return stats[0];
    }

#if UNITY_EDITOR
    void Update()
    {
        if (DarDano) //Temporário
        {
            DarDano = !DarDano;
            InterfaceHelper.GetDamageable(gameObject).TakeDamage(Dano); //Exemplo para dar dano
        }
    }
#endif

    void Start()
    {

    }

}
