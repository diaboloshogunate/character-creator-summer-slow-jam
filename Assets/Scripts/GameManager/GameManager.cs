using System.Collections.Generic;
using DefaultNamespace;
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
        
        public GameState GameState { get; private set; } = null;
        public EventManager EventManager { get; private set; } = new EventManager();
        public Tilemap Tilemap { get; private set; } = new Tilemap();
        public List<Entity> PlayerPieces { get; private set; } = new List<Entity>();
        public List<Entity> EnemyPieces { get; private set; } = new List<Entity>();

        private void Awake()
        {
            GameState = new PlayerBeginState();
            GameState.Enter(null, this);
        }
    }
}