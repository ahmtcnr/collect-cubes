using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Fountain Behaviours/Left-Right", fileName = "Left-Right")]
public class FountainLeftRight : FountainPatternBase
{
    public override IEnumerator CO_Behaviour(FountainSettings fountainSettings)
    {
        var collectableCount = 0;
        while (true)
        {
            CreateAndPush(fountainSettings, fountainSettings.SpawnPosition + Vector3.right, Vector3.right);
            CreateAndPush(fountainSettings, fountainSettings.SpawnPosition + Vector3.left, Vector3.left);

            collectableCount += 2;

            if (collectableCount > fountainSettings.MaximumCollectableAmount)
            {
                break;
            }

            yield return new WaitForSeconds(fountainSettings.SpawnInterval);
        }
    }
}