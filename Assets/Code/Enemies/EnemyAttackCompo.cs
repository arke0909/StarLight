using Code.Combat;
using UnityEngine;

namespace Code.Enemies
{
    public class EnemyAttackCompo : MonoBehaviour
    {
        [SerializeField] private Transform targetTrm;
        [SerializeField] private Transform firePosTrm;
        [SerializeField] private Bullet bulletPrefab;

        public void Fire()
        {
            foreach (Transform firePos in firePosTrm)
            {
                Bullet bullet = Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
                bullet.InitBullet(targetTrm.position - firePos.position);
            }
        }
    }
}