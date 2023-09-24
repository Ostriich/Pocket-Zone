using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _tracer, _fire;
    [SerializeField] private GameObject _startPoint, _shootPoint;
    [SerializeField] private GameObject _countBullets;
    [SerializeField] private float _damage;
    [SerializeField] private float _reload, _reloadTime;
    [SerializeField] private float _distanceAutoShooting;
    public bool IsAutoShooting { get; private set; }

    private void FixedUpdate()
    {
        _reload += Time.deltaTime;

        GameObject enemy = CheckEnemiesClose();
        if (enemy != null)
        {
            IsAutoShooting = true;

            if (_reload >= _reloadTime)
            {
                _reload = 0;
                LookAtTheEnemy(enemy);
                Shoot();
            }
        }
        else
            IsAutoShooting = false;
    }

    // Spawn tracer and fire shot, identify damaged enemies
    public void Shoot()
    {
        if (_countBullets.GetComponent<Text>().text != "0")
        {
            FindHittedEnemies((_shootPoint.transform.position - _startPoint.transform.position), _startPoint.transform.position);

            GameObject _tracerInst = Instantiate(_tracer, _shootPoint.transform.position, Quaternion.identity);
            _tracerInst.GetComponent<TracerBehaviour>().Direction = _shootPoint.transform.position 
                - _startPoint.transform.position;

            Instantiate(_fire, _shootPoint.transform.position, Quaternion.identity);

            _countBullets.GetComponent<Text>().text = (int.Parse(_countBullets.GetComponent<Text>().text) - 1).ToString();
        }
    }

    // Deals damage to all enemies on the tracer
    private void FindHittedEnemies(Vector3 direction, Vector3 shootPos)
    {
        foreach (RaycastHit hitEnemy in Physics.RaycastAll(shootPos, direction))
        {
            hitEnemy.collider.gameObject.GetComponent<EnemyBehaviour>().GetDamage(_damage);
        }
    }

    // Find enemies in close distance
    private GameObject CheckEnemiesClose()
    {
        float minDistance = _distanceAutoShooting;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= minDistance)
            {
                closestEnemy = enemy;
                minDistance = Vector3.Distance(transform.position, enemy.transform.position);
            }
        }

        return (closestEnemy);
    }

    // Change orientation towards the closest enemy
    private void LookAtTheEnemy(GameObject enemy)
    {
        if (transform.position.x - enemy.transform.position.x < 0)
            GetComponent<Animator>().SetBool("IsMoveRight", true);
        else
            GetComponent<Animator>().SetBool("IsMoveRight", false);
    }
}
