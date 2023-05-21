using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Data", order = 1, fileName = "New Level")]
public class LevelData : ScriptableObject
{
   public int levelNumber;
   public GameObject LevelPrefab;
}
