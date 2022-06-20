using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IUnitActions
    {
        Task Move(Vector3 destination);
        Task Damage(int amt);
        Task Heal(int amt);
        Task Die();
        Task Attack(Unit unit);
        Task Refresh();
    }
}