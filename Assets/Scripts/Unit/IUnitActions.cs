using UnityEngine;

namespace DefaultNamespace
{
    public interface IUnitActions
    {
        void Move(Vector3 destination);
        void Damage(int amt);
        void Heal(int amt);
        void Die();
        void Attack(Unit unit);
        void Refresh();
    }
}