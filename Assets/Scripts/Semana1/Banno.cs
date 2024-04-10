using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banno : State
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

            playerStats.energy = Mathf.Clamp(playerStats.energy - Random.Range(4, 15), 0, 100);

            playerStats.hunger = Mathf.Clamp(playerStats.hunger - Random.Range(2, 5), 0, 100);

            playerStats.bathroom = Mathf.Clamp(playerStats.bathroom - Random.Range(10, 20), 0, 100);

            if (playerStats.bathroom == 0 && playerStats.energy > 0 && playerStats.hunger > 0)
            {
                m_MachineState.NextState(TypeState.Jugar);
                playerController.Move(TypePath.Jugar);
            }
            else if (playerStats.bathroom == 0 && playerStats.energy == 0)
            {
                m_MachineState.NextState(TypeState.Dormir);
                playerController.Move(TypePath.Dormir);
            }
            else if (playerStats.bathroom == 0 && playerStats.hunger == 0)
            {
                m_MachineState.NextState(TypeState.Comer);
                playerController.Move(TypePath.Comer);
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
