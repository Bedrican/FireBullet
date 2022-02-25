using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    [Tooltip("Enemy prefab")]
    public GameObject Enemy;
    
    public float health=100f;
    
    public float enemySpeed = 1f;
}
