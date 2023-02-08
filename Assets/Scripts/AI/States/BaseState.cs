
public abstract class BaseState
{
    protected EnemyController Controller;

    protected BaseState(EnemyController controller) => Controller = controller;


    public virtual void EnterState()
    {
        
    }
    public virtual void UpdateState(){}
    public virtual void ExitState(){}

}