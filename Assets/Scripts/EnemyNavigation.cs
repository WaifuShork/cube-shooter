using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Shork
{
    public class EnemyNavigation : MonoBehaviour
    {
        public Transform[] points;
        public GameObject enemyPrefab;
        public Transform playerTransform;
        private NavMeshAgent navMesh;
        public Transform enemyTransform;

        private void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            navMesh.autoBraking = false;
        }


        public void Update()
        {
            MoveTowardPlayer();
            
        }

        private void MoveTowardPlayer()
        {
            navMesh.destination = playerTransform.position;
        }

        private void SpawnEnemies()
        {
            //Instantiate(enemyPrefab, new Vector3(0, 5, 0), Quaternion.identity);

        }

       


    } 
}
