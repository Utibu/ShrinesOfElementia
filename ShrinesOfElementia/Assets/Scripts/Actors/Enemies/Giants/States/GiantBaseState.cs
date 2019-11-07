// Author: Bilal El Medkouri

public class GiantBaseState : State
{
    protected Giant owner;

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Giant)owner;
    }
}
