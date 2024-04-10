using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dormir : State
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

            playerStats.energy = Mathf.Clamp(playerStats.energy + Random.Range(4, 10), 0, 100);

            playerStats.hunger = Mathf.Clamp(playerStats.hunger - Random.Range(1, 3), 0, 100);

            if (playerStats.energy == 100 && playerStats.hunger > 0)
            {
                m_MachineState.NextState(TypeState.Jugar);
                playerController.Move(TypePath.Jugar);
            }
            else if (playerStats.energy == 100 && playerStats.hunger == 0)
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
