using UnityEngine;

namespace Code.Combat
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected int maxHitCount = 1; //최대 피격 가능 객체 수
        [SerializeField] protected LayerMask whatIsTarget;

        public virtual void Initialize()
        {
            
        }
        public abstract bool CastDamage(float damage);
    }
}