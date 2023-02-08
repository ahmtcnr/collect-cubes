using UnityEngine;

namespace AI.States
{
    public class Idle : BaseState
    {
        private float elapsedTime = 0;

        public Idle(EnemyController controller) : base(controller)
        {
        }
        public override void EnterState()
        {
            base.EnterState();
            Controller.SetGhostObjectPosition(Controller.transform.position);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= Controller._enemySettings.IdleDuration)
            {
                Controller.SwitchStates(AIState.setPath);
            }
        }
    }
}