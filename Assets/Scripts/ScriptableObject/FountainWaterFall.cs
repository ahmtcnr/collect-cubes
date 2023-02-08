using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "WaterFall", menuName = "Fountain Behaviours/WaterFall", order = 0)]
public class FountainWaterFall : FountainPatternBase
{
    public override IEnumerator CO_Behaviour(FountainSettings fountainSettings)
    {
        var collectableCount = 0;
        while (true)
        {
            for (int i = -3; i <= 3; i++)
            {
                var spawnPos = fountainSettings.SpawnPosition;
                spawnPos.x = i;
                
                
                CreateAndPush(fountainSettings, spawnPos, Vector3.down);
                collectableCount++;
            }


            if (collectableCount > fountainSettings.MaximumCollectableAmount)
            {
                break;
            }

            yield return new WaitForSeconds(fountainSettings.SpawnInterval);
        }
    }
}