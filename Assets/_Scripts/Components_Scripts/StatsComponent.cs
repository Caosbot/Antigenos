using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour, IDamageable
{
    [System.Serializable]
    private class Stat //Classe que define os Stats do Jogo
    {
        [SerializeField] public string name { get; } //Nome do Stat, n�o vai ser mudado durante o jogo!
        [SerializeField] private int baseValue; //Valor base do Stat para cada Objeto, ele define o valor M�ximo e o Atual(O Atual s� segue o valor base na Inicializa��o)
        [System.NonSerialized] public int currentValue; //Valor Atual
        [System.NonSerialized] protected int maxValue; //Valor M�ximo
        public Stat(string StatName, int BaseValue) //Construtor dos Stats
        {
            name = StatName;
            baseValue = BaseValue;
            maxValue = baseValue;
            currentValue = baseValue;
        }
    }

    //Declara��o de Essenciais como padr�o
    [SerializeField] private Stat[] stats = { new Stat("Vida", 20), new Stat("Estamina", 20), new Stat("Mana", 10)}; //Os Stats do Jogo, por padr�o todos os scripts cont�m 3

#if UNITY_EDITOR
    [SerializeField] private bool DarDano;
    [SerializeField] private int Dano;
#endif

    public void TakeDamage(int inDamage, bool ignoreArmor = false) //M�todo da Interface IDamageable
    {
        Stat tempStat = FindDesiredStat("Vida");
        tempStat.currentValue -= inDamage;
        Debug.Log("Vida atual: " + tempStat.currentValue);
    }
    private Stat FindDesiredStat(string statName) //M�todo que retorna o Stat baseado no seu nome, caso inv�lido retorna o primeiro Stat e Printa no Console Erro
    {
        foreach(Stat currentStat in stats)
        {
            if(statName == currentStat.name)
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
        if (DarDano) //Tempor�rio
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
