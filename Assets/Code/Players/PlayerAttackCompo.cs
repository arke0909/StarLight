using Code.Combat;
using UnityEngine;

namespace Code.Players
{
    public class PlayerAttackCompo : MonoBehaviour
    {
        [SerializeField] private Transform targetTrm;
        [SerializeField] private Transform firePosTrm;
        [SerializeField] private float fireRate = 0.2f;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private LayerMask whatIsTarget;

        float _currentTime = 0f;
        
        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (fireRate <= _currentTime)
            {
                _currentTime = 0;
                Bullet bullet = Instantiate(bulletPrefab, firePosTrm.position, Quaternion.identity);
                bullet.InitBullet(firePosTrm.forward);
            }
        }

        private void FixedUpdate()
        {
            Ray ray = Camera.main.ScreenPointToRay(playerInput.MouseVector);

            if (Physics.Raycast(ray, out RaycastHit hit, Camera.main.farClipPlane, whatIsTarget))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    targetTrm.LookAt(hit.collider.transform);
                }
                else
                {
                    Quaternion targetRot = Quaternion.LookRotation(hit.point - targetTrm.position);
                    targetTrm.rotation = targetRot;
                }
            }
        }
    }
}