public class JumpAction : ActionBase
{

    public JumpAction(Unit unit) : base(unit)
    {

    }

    public override void Execute()
    {
        Unit.Jump();
    }

    public override void Undo()
    {
        Unit.Jump();
    }
}
