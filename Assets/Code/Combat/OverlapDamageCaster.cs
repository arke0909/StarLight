using UnityEngine;

namespace Code.Combat
{
    
    public class OverlapDamageCaster : DamageCaster
    {
        public enum OverlapCastType
        {
            Circle, Box
        }
        [SerializeField] protected OverlapCastType overlapCastType;
        [SerializeField] private Vector2 damageBoxSize;
        [SerializeField] private float damageRadius;

        private Collider[] _hitResults;

        public override void Initialize()
        {
            _hitResults = new Collider[maxHitCount];
        }

        public void SetRadius(float radius)
        {
            damageRadius = radius;
        }
        public override bool CastDamage(float damage)
        {
            
            int cnt = overlapCastType switch
            {
                OverlapCastType.Circle => Physics.OverlapSphereNonAlloc(transform.position, damageRadius, _hitResults,whatIsTarget),
                //OverlapCastType.Box => Physics2D.overlap(transform.position, damageBoxSize, 0, whatIsTarget, _hitResults),
                _ => 0
            };
            
            for (int i = 0; i < cnt; i++)
            {
                if (_hitResults[i].TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damage);
                }
            }
            return cnt > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.7f, 0.7f, 0, 1f);
            switch(overlapCastType)
            {
                case OverlapCastType.Circle:
                    Gizmos.DrawWireSphere(transform.position, damageRadius);
                    break;
                case OverlapCastType.Box:
                    Gizmos.DrawWireCube(transform.position, damageBoxSize);
                    break;
            }
        }
#endif
    }
}