using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerState : State<SpawnerStateEnum>
{
    protected ZombieSpawner Spawner;
    protected SpawnerState(ZombieSpawner spawner, StateMachine<SpawnerStateEnum> stateMachine) : base(stateMachine)
    {
        Spawner = spawner;
    }


    protected void SpawnZombie()
    {
        GameObject ZombieToSpawn = Spawner.ZombiePrefab[Random.Range(0, Spawner.ZombiePrefab.Length)];
        SpawnerVolume spawnVolume = Spawner.SpawnVolume[Random.Range(0, Spawner.SpawnVolume.Length)];

        if (!Spawner.FollowTarget) return;

        GameObject zombie = Object.Instantiate(ZombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);


        zombie.GetComponent<ZombieComponent>().Initialize(Spawner.FollowTarget);


    }
}
