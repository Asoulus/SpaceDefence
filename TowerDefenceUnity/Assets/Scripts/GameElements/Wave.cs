using UnityEngine;

[CreateAssetMenu(fileName = "WaveScriptableObject", menuName = "ScriptableObjects/Wave")]
public class Wave : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public int spawnDelayInMiliseconds;
}
