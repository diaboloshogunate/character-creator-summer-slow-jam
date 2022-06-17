using System;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null) _instance = new GameManager();
                return _instance;
            }
        }
        
        public EventManager EventManager { get; private set; } = new EventManager();
        [field: SerializeField] public Tilemap Tilemap { get; private set; } = null;
        public List<Entity> PlayerPieces { get; private set; } = new List<Entity>();
        public List<Entity> EnemyPieces { get; private set; } = new List<Entity>();

        [field: Header("Initial spawns - for debugging")]
        [field: SerializeField] private GameObject playerSpawnEntity { get; set; }= null;
        [field: SerializeField] public List<Transform> PlayerSpawnPoints { get; private set; } = new List<Transform>();
        [field: SerializeField] private GameObject EnemySpawnEntity { get; set; } = null;
        [field: SerializeField] public List<Transform> EnemySpawnPoints { get; private set; } = new List<Transform>();

        private void Awake()
        {
        }

        private void Start()
        {
            SpawnPieces();
        }

        private void SpawnPieces()
        {
            foreach (Transform playerSpawnPoint in PlayerSpawnPoints)
            {
                GameObject obj = Spawn(playerSpawnEntity, playerSpawnPoint.position);
                if(obj) PlayerPieces.Add(obj.GetComponent<Entity>());
            }

            foreach (Transform enemySpawnPoint in EnemySpawnPoints)
            {
                GameObject obj = Spawn(EnemySpawnEntity, enemySpawnPoint.position);
                if(obj) EnemyPieces.Add(obj.GetComponent<Entity>());
            }
        }

        [CanBeNull]
        public GameObject Spawn(GameObject prefab, Vector3 position)
        {
            Entity entity = prefab.GetComponent<Entity>();
            Vector3Int tilePosition = Tilemap.WorldToCell(position);
            Vector3 tileCenterPosition = Tilemap.GetCellCenterWorld(tilePosition);

            if (!entity)
            {
                Debug.LogErrorFormat("Can not spawn this prefab. It must have the Entity Component. Prefab {0}", prefab.name);
                return null;
            }

            GraphNode node = AstarPath.active.GetNearest(tileCenterPosition).node;
            if (!node.Walkable)
            {
                Debug.LogErrorFormat("The nearest node is not walkable. Position {0}", transform.ToString());
                return null;
            }

            GameObject obj = Instantiate(prefab);
            obj.transform.position = (Vector3) node.position;

            return obj;
        }
    }
}