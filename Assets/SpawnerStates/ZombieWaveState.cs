using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWaveState : SpawnerState
{

    public int ZombiesToSpawn = 5;
    public SpawnerStateEnum NextState = SpawnerStateEnum.Intermediate;
    private int TotalZombiesKilled = 0;

    public ZombieWaveState(ZombieSpawner spawner, StateMachine<SpawnerStateEnum> stateMachine) : base(spawner, stateMachine)
    {
    }

    public override void Start()
    {
        base.Start();
        for (int i = 0; i < ZombiesToSpawn; i++)
        {
            SpawnZombie();
        }
    }

    protected override void OnZombieDeath()
    {
        base.OnZombieDeath();
        Debug.Log("Zombie Killed");
        TotalZombiesKilled++;
        if (TotalZombiesKilled < ZombiesToSpawn) return;

        StateMachine.ChangeState(NextState);
        Spawner.CompleteWave(NextState);
    }
}
