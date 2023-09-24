using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float _fullHealth, _currentHealth;
    [SerializeField] private GameObject _healthSlider;

    [SerializeField] private float _damage;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedAttack, _attackColdown;
    [SerializeField] private float _minTriggerDistance, _maxTriggerDistance;

    private void FixedUpdate()
    {
        // Displaying the current health on the strip
        _healthSlider.GetComponent<Slider>().value = _currentHealth / _fullHealth;

        // Death at 0 health
        if (_currentHealth <= 0)
            Death();

        // Movement to the player
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (Vector2.Distance(transform.position, target.transform.position) <= _maxTriggerDistance &&
            Vector2.Distance(transform.position, target.transform.position) >= _minTriggerDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speedMove / 100);

            GetComponent<Animator>().SetBool("IsMove", true);
        }
        else
            GetComponent<Animator>().SetBool("IsMove", false);

        AnimationControl(target);

        // Attack
        _attackColdown += Time.deltaTime;

        if (_attackColdown >= _speedAttack &&
            Vector2.Distance(transform.position, target.transform.position) <= _minTriggerDistance)
        {
            Attack(target, _damage);
            _attackColdown = 0;
        }
    }

    // Taking damage from player
    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
    }

    private void AnimationControl(GameObject target)
    {
        if (target.transform.position.x - transform.position.x <= 0)
            GetComponent<Animator>().SetBool("IsMoveRight", false);
        else
            GetComponent<Animator>().SetBool("IsMoveRight", true);
    }

    private void Attack(GameObject target, float damage)
    {
        target.GetComponent<PlayerHealth>().GetDamage(damage);
    }

    private void Death()
    {
        GetComponent<LootEnemy>().DropLoot();
        Destroy(gameObject);
    }
}
