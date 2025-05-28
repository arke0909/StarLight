using System.Collections;
using Code.Players;
using UnityEngine;

namespace Code.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private WayPoints wayPoints;
        [SerializeField] private EnemyAttackCompo attackCompo;
        [SerializeField] private float stopOffset = 0.1f;
        [SerializeField] private float moveSpeed = 5f;
        private Rigidbody _rbCompo;
        private Vector3 _destination;
        private int pointIdx = 0;
        public bool IsArrived { get; private set; } = false;

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            StartCoroutine(PatrolCoroutine());
        }

        private IEnumerator PatrolCoroutine()
        {
            while (true)
            {
                IsArrived = false;
                int randomIdx = Random.Range(0, wayPoints.Length);
                Vector3 startPos = transform.position;
                _destination = wayPoints[randomIdx].transform.position;
                float totalTime = Vector3.Distance(startPos, _destination) / moveSpeed;
                float currentTime = 0;
                float percent = 0;
                while (percent <= 1)
                {
                    currentTime += Time.deltaTime;
                    percent = currentTime / totalTime;
                    transform.position = Vector3.Lerp(startPos, _destination, percent);
                    yield return null;
                }

                IsArrived = true;
                attackCompo.Fire();
                yield return new WaitForSeconds(5);
                
            }
        }

        
    }
}