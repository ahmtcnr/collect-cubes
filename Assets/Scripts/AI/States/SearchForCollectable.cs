namespace AI.States
{
    public class SearchForCollectable : BaseState
    {
        public SearchForCollectable(EnemyController controller) : base(controller)
        {
        }
        public override void UpdateState()
        {
            base.UpdateState();
            Controller._pathfindingManager.GetRichestNode();
        }
    }
}