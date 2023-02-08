using UnityEngine;

namespace AI.States
{
    public class ReturnToBase : BaseState
    {
        private float elapsedTime = 0;

        public ReturnToBase(EnemyController controller) : base(controller)
        {
        }

        public override void EnterState()
        {
            base.EnterState();

            elapsedTime = 0;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (elapsedTime >= Controller._enemySettings.TimeDelayBetweenNodes)
            {
                if (Controller.ReturnNodes.Count != 0)
                {
                    Node node = Controller.ReturnNodes.Pop();
                    Controller.SetGhostObjectPosition(node.transform.position);
                }
                else
                {
                    Controller.SwitchStates(AIState.idle);
                }


                elapsedTime = 0;
            }

            elapsedTime += Time.deltaTime;
        }
    }
}