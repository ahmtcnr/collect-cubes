using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Half Circle", menuName = "Fountain Behaviours/Half Circle", order = 0)]
public class FountainHalfCircle : FountainPatternBase
{
    public override IEnumerator CO_Behaviour(FountainSettings fountainSettings)
    {
        var start = Vector3.right;
        var angle = 0f;
        var isClockWise = false;
        var collectableCount = 0;
        while (true)
        {
            var direction = Quaternion.AngleAxis(angle, Vector3.down) * start;

            CreateAndPush(fountainSettings, fountainSettings.SpawnPosition, direction);

            if (angle > 180)
                isClockWise = false;
            if (angle < 0)
                isClockWise = true;
            if (isClockWise)
                angle += 15;
            else
                angle -= 15;

            collectableCount++;


            if (collectableCount > fountainSettings.MaximumCollectableAmount)
            {
                break;
            }

            yield return new WaitForSeconds(fountainSettings.SpawnInterval);
        }
    }
}