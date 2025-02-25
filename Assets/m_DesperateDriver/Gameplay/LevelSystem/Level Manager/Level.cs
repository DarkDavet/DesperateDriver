using System;
using UnityEngine;


    public class Level : MonoBehaviour
    {
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField, Range(1,20)] private int levelIndex;
    public bool isPlayable = false;

    private void Start()
    {
        isPlayable = false;
    }
    public int GetLevelIndex()
    {
        return levelIndex;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (playerSpawnPoint != null)
        {
            Gizmos.color = Color.magenta;
            var m = Gizmos.matrix;
            Gizmos.matrix = playerSpawnPoint.localToWorldMatrix;
            Gizmos.DrawSphere(Vector3.up * 0.5f + Vector3.forward, 0.5f);
            Gizmos.DrawCube(Vector3.up * 0.5f, Vector3.one);
            Gizmos.matrix = m;
        }
    }
#endif
    }
