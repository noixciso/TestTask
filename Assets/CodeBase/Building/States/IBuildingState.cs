namespace CodeBase.Building.States
{
    public interface IBuildingState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}