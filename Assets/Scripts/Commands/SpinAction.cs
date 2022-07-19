public class SpinAction : ActionBase
{
    public SpinAction(Unit unit) : base(unit)
    {

    }

    public override void Execute()
    {
        Unit.Spin();
    }

    public override void Undo()
    {
        Unit.Spin();
    }
}
