public abstract class ActionBase
{
    protected readonly Unit Unit;

    protected ActionBase(Unit unit)
    {
        Unit = unit;
    }

    public abstract void Execute();
    public abstract void Undo();
}
