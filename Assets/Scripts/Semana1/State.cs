using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeState { Comer, Jugar, Banno, Dormir }
public class State : MonoBehaviour
{
    public TypeState type;
    public MachineState m_MachineState;
    [SerializeField] protected StatsPlayer playerStats;

    [SerializeField]
    protected float[] arrayTime = new float[10];
    protected float FrameRate;
    protected int index = 0;
    public virtual void LoadComponent()
    {
        m_MachineState = GetComponent<MachineState>();
        playerStats = GetComponent<StatsPlayer>();
    }
    public virtual void Enter()
    {

    }
    public virtual void Execute()
    {

    }
    public virtual void Exit()
    {

    }
    public void RandeArray()
    {
        for (int i = 0; i < arrayTime.Length; i++)
        {
            arrayTime[i] = UnityEngine.Random.Range(4, 10);
        }
    }
}
