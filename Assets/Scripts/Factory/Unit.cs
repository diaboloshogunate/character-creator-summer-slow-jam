using DefaultNamespace;
using UnityEngine;

namespace Factory
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Factories/Unit")]
    public class Unit : ScriptableObject
    {
        [field: SerializeField] private GameObject EnemyUnit { get; set; }
        [field: SerializeField] private GameObject PlayerUnit { get; set; }

        public Entity BuildUnit(bool isPlayerUnit)
        {
            GameObject obj = Instantiate(isPlayerUnit ? PlayerUnit : EnemyUnit);

            return obj.GetComponent<Entity>();
        }
    }
}