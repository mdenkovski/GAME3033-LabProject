using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
    private ZombieStateMachine ZombieStateMachine;

    private void Awake()
    {
        ZombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    public override void Destroy()
    {
        //base.Destroy();
        ZombieStateMachine.ChangeState(ZombieStateType.Dead);
    }
}
