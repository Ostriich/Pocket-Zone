using System.Collections.Generic;
using UnityEngine;

public class LootEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _loot = new List<GameObject>();
    [SerializeField] private float _dropForce;

    // leaves random loot after death
    public void DropLoot()
    {
        int lootnumber = Random.Range(0, _loot.Count);

        GameObject droppedLoot = Instantiate(_loot[lootnumber], transform.position, Quaternion.identity);

        // Scattering effect
        Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        droppedLoot.GetComponent<Rigidbody2D>().AddForce(dropDirection * _dropForce);
    }
}
