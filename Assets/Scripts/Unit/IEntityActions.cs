using UnityEngine;

namespace DefaultNamespace
{
    public interface IEntityActions
    {
        void Move(Vector3 destination);
        void Damage(int amt);
        void Heal(int amt);
        void Die();
        void Attack(Entity entity);
        void Refresh();
    }
}