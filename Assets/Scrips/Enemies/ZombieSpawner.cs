
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ZombieSpawnerStateMachine))]
public class ZombieSpawner : MonoBehaviour
{

    [SerializeField]
    private int NumberOfZombiesToSpawn;

    public GameObject[] ZombiePrefab;

    public SpawnerVolume[] SpawnVolume;

    public GameObject FollowTarget => FollowGameObject;
    private GameObject FollowGameObject;

    private ZombieSpawnerStateMachine StateMachine;

    // Start is called before the first frame update
    void Start()
    {
        FollowGameObject = GameObject.FindGameObjectWithTag("Player");


        StateMachine = GetComponent<ZombieSpawnerStateMachine>();
        ZombieWaveState BeginnerWave = new ZombieWaveState(this, StateMachine)
        {
            ZombiesToSpawn = 10,
            NextState = SpawnerStateEnum.Complete
        };

        StateMachine.AddState(SpawnerStateEnum.Beginner, BeginnerWave);
        StateMachine.Initialize(SpawnerStateEnum.Beginner);


    }

    private void SpawnZombie()
    {
        GameObject ZombieToSpawn = ZombiePrefab[Random.Range(0, ZombiePrefab.Length)];
        SpawnerVolume spawnVolume = SpawnVolume[Random.Range(0, SpawnVolume.Length)];

        if (!FollowGameObject) return;

        GameObject zombie = Instantiate(ZombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);


        zombie.GetComponent<ZombieComponent>().Initialize(FollowGameObject);


    }

}
