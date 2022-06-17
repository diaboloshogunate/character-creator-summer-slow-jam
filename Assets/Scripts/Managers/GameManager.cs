using System.Collections.Generic;
using State.Turn;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public Tilemap Tilemap { get; private set; }
        
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

        public TurnStateMachine TurnManager { get; private set; } = new TurnStateMachine();
        
        private Context Context { get; set; }

        private List<Entity> PlayerUnits { get; set; } = new List<Entity>();

        private List<Entity> EnemyUnits { get; set; } = new List<Entity>();

        private void Awake()
        {
            Context = new Context(TurnManager, false, EnemyUnits, PlayerUnits);
            TurnManager.Transition(new BeginPhase(Context));
        }
    }
}