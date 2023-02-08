using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    public class SetPath : BaseState
    {
        private float elapsedTime = 0;
        public SetPath(EnemyController controller) : base(controller)
        {
        }
        private Queue<Node> _targetPath = new Queue<Node>();
        public override void EnterState()
        {
            base.EnterState();
            _targetPath = Controller._pathfindingManager.GetClosestPath();
            elapsedTime = 0;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (elapsedTime >= Controller._enemySettings.TimeDelayBetweenNodes)
            {
                if (_targetPath.Count != 0)
                {
                    Node node = _targetPath.Dequeue();
                    Controller.ReturnNodes.Push(node);
                    Controller.SetGhostObjectPosition(node.transform.position);
                }
                else
                {
                    Controller.SwitchStates(AIState.returnToBase);
                }
                elapsedTime = 0;
            }
            elapsedTime += Time.deltaTime;
        }
    }
}