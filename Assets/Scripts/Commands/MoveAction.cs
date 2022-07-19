public class MoveAction : ActionBase
{
    private readonly Direction _direction;
    public MoveAction(Unit unit, Direction direction) : base(unit)
    {

    }

    public override void Execute()
    {
        Unit.Move(_direction);
    }

    public override void Undo()
    {
        Unit.Move(Direction.GetOpposite(_direction));
    }
}
