
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ZombieSpawnerStateMachine))]
public class ZombieSpawner : MonoBehaviour , ISavable
{

    public delegate void WaveComplete(SpawnerStateEnum currentState);
    public event WaveComplete OnWaveComplete;


    [SerializeField]
    private int NumberOfZombiesToSpawn;

    public GameObject[] ZombiePrefab;

    public SpawnerVolume[] SpawnVolume;

    public GameObject FollowTarget => FollowGameObject;
    private GameObject FollowGameObject;

    private ZombieSpawnerStateMachine StateMachine;

    private SpawnerStateEnum StartingState = SpawnerStateEnum.Beginner;

    // Start is called before the first frame update
    void Start()
    {
        FollowGameObject = GameObject.FindGameObjectWithTag("Player");
        StateMachine = GetComponent<ZombieSpawnerStateMachine>();


        ZombieWaveState BeginnerWave = new ZombieWaveState(this, StateMachine)
        {
            ZombiesToSpawn = 5,
            NextState = SpawnerStateEnum.Intermediate
        };
        StateMachine.AddState(SpawnerStateEnum.Beginner, BeginnerWave);

        ZombieWaveState IntermediateWave = new ZombieWaveState(this, StateMachine)
        {
            ZombiesToSpawn = 10,
            NextState = SpawnerStateEnum.Complete
        };
        StateMachine.AddState(SpawnerStateEnum.Intermediate, IntermediateWave);


        StateMachine.Initialize(StartingState);

    }

    public void CompleteWave(SpawnerStateEnum nextState)
    {
        OnWaveComplete?.Invoke(nextState);

    }

    private void SpawnZombie()
    {
        GameObject ZombieToSpawn = ZombiePrefab[UnityEngine.Random.Range(0, ZombiePrefab.Length)];
        SpawnerVolume spawnVolume = SpawnVolume[UnityEngine.Random.Range(0, SpawnVolume.Length)];

        if (!FollowGameObject) return;

        GameObject zombie = Instantiate(ZombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);


        zombie.GetComponent<ZombieComponent>().Initialize(FollowGameObject);


    }

    public SaveDataBase SaveData()
    {
        SpawnerSaveData saveData = new SpawnerSaveData
        {
            Name = gameObject.name,
            CurrentState = StateMachine.ActiveEnumState

        };
        return saveData;
    }

    public void LoadData(SaveDataBase saveData)
    {
        SpawnerSaveData spawnerSaveData = (SpawnerSaveData)saveData;
        StartingState = spawnerSaveData.CurrentState;
    }

    

}

[Serializable]
public class SpawnerSaveData : SaveDataBase
{
    public SpawnerStateEnum CurrentState;
}
