using System.Collections.Generic;
using DefaultNamespace;
using Factory;
using UnityEngine;

namespace State.Turn
{
    public class Context
    {
        public int Turn { get; private set; }
        public TurnStateMachine StateMachine { get; private set; }
        public List<Entity> OurUnits { get; private set; }
        public List<Entity> EnemyUnits { get; private set; }
        public bool IsPlayersTurn { get; private set; }
        public Unit UnitFactory { get; private set; }
        public Collider EnemySpawnBounds { get; private set; }
        public Collider PlayerSpawnBounds { get; private set; }
        
        public LayerMask UnitLayersMask { get; private set; }

        public Context(TurnStateMachine stateMachine, Unit unitFactory, Collider enemySpawnBounds, Collider playerSpawnBounds, int turn, bool isPlayersTurn, List<Entity> ourUnits, List<Entity> enemyUnits, LayerMask unitLayersMask)
        {
            StateMachine      = stateMachine;
            UnitFactory       = unitFactory;
            IsPlayersTurn     = isPlayersTurn;
            OurUnits          = ourUnits;
            EnemyUnits        = enemyUnits;
            UnitLayersMask    = unitLayersMask;
            Turn              = turn;
            EnemySpawnBounds  = enemySpawnBounds;
            PlayerSpawnBounds = playerSpawnBounds;
        }
    }
}