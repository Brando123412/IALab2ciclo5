using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugar : State
{
    void Start()
    {
        RandeArray();
        LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    public override void Enter()
    {

    }
    public override void Execute()
    {
        if (FrameRate > arrayTime[index])
        {
            FrameRate = 0;
            index++;
            if (index == arrayTime.Length)
                RandeArray();
            index = index % arrayTime.Length;

            playerStats.energy = Mathf.Clamp(playerStats.energy - Random.Range(4, 10), 0, 100);
            if (playerStats.energy == 0)
            {
                m_MachineState.NextState(TypeState.Dormir);
                return;
            }
            playerStats.hunger = Mathf.Clamp(playerStats.hunger - Random.Range(4, 10), 0, 100);
            if (playerStats.hunger == 0)
            {
                m_MachineState.NextState(TypeState.Comer);
                return;
            }
        }
        FrameRate += Time.deltaTime;
    }
    public override void Exit()
    {

    }
    void Update()
    {
        Execute();
    }
}
