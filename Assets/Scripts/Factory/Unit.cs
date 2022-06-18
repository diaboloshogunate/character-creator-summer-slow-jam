using DefaultNamespace;
using UnityEngine;

namespace Factory
{
    public class Unit : MonoBehaviour
    {
        [field: SerializeField] private GameObject EnemyUnit { get; set; }
        [field: SerializeField] private GameObject PlayerUnit { get; set; }

        public DefaultNamespace.Unit BuildUnit(bool isPlayerUnit)
        {
            GameObject obj = Instantiate(isPlayerUnit ? PlayerUnit : EnemyUnit);

            return obj.GetComponent<DefaultNamespace.Unit>();
        }
    }
}