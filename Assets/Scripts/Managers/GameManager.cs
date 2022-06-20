using System;
using System.Collections.Generic;
using Factory;
using State.Turn;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [field: SerializeField] public Tilemap Tilemap { get; private set; }

        [field: SerializeField] public Factory.Unit UnitFactory { get; private set; }
        
        [field: SerializeField] public Collider PlayerSpawnBounds { get; private set; }

        [field: SerializeField] public Collider EnemySpawnBounds { get; private set; }
        
        [field: SerializeField] public LayerMask UnitLayersMask { get; private set; }
        
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

        private List<Unit> PlayerUnits { get; set; } = new List<Unit>();

        private List<Unit> EnemyUnits { get; set; } = new List<Unit>();

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            Context = new Context(
                TurnManager, 
                UnitFactory, 
                EnemySpawnBounds, 
                PlayerSpawnBounds, 
                1,
                false, 
                EnemyUnits, 
                PlayerUnits, 
                UnitLayersMask,
                this
            );
            TurnManager.Transition(new BeginPhase(Context));
        }
        

        public void NextTurn(Context context)
        {
            Context = new Context(
                TurnManager, 
                UnitFactory, 
                EnemySpawnBounds, 
                PlayerSpawnBounds, 
                context.IsPlayersTurn ? Context.Turn : Context.Turn + 1,
                !context.IsPlayersTurn, 
                PlayerUnits, 
                EnemyUnits, 
                UnitLayersMask,
                this
            );
            TurnManager.Transition(new BeginPhase(Context));
        }
    }
}