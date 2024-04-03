using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comer : State
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

            playerStats.hunger = Mathf.Clamp(playerStats.hunger + Random.Range(4, 10), 0, 100);

            playerStats.bathroom = Mathf.Clamp(playerStats.bathroom + Random.Range(4, 10), 0, 100);

            if (playerStats.bathroom == 100)
            {
                m_MachineState.NextState(TypeState.Banno);
                return;
            }
            if (playerStats.energy == 0 && playerStats.hunger == 100)
            {
                m_MachineState.NextState(TypeState.Dormir);
            }
            else if (playerStats.hunger == 100 && playerStats.energy > 0)
            {
                m_MachineState.NextState(TypeState.Jugar);
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
