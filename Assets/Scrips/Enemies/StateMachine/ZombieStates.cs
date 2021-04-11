public class ZombieStates : State<ZombieStateType>
{
    protected ZombieComponent OwnerZombie;
    public ZombieStates(ZombieComponent zombie, ZombieStateMachine statemachine) : base(statemachine)
    {
        OwnerZombie = zombie;
    }
}


public enum ZombieStateType
{
    Idle,
    Attack,
    Follow,
    Dead
}