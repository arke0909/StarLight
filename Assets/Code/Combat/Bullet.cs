using UnityEngine;

namespace Code.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float lifeTime = 5f;
        private Rigidbody _rbCompo;
        private float _currentLifeTime = 0;

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody>();
        }

        public void InitBullet(Vector3 direction)
        {
            _rbCompo.linearVelocity = direction.normalized * moveSpeed;
        }

        private void Update()
        {
            _currentLifeTime += Time.deltaTime;
            if(lifeTime <= _currentLifeTime)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}