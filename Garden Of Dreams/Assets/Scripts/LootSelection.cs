using UnityEngine;

public class LootSelection : MonoBehaviour
{
    [SerializeField] private float _triggerDistance;
    [SerializeField] private float _speedMove;

    private void FixedUpdate()
    {
        // Movement to the player
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (Vector2.Distance(transform.position, target.transform.position) <= _triggerDistance && IsPlaceInBag())
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, _speedMove / 100);
    }

    // Add loot in bag
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlaceInBag())
        {
            BagController bag = collision.gameObject.GetComponent<PlayerMove>().Bag;
            for (int i = 0; i < bag.LootImages.Length; i++)
            {
                if (bag.LootImages[i] == GetComponent<SpriteRenderer>().sprite)
                {
                    bag.CountLoot[i] += 1;
                    bag.SaveBag();
                    Destroy(gameObject);
                    return;
                }
            }
            for (int i = 0; i < bag.LootImages.Length; i++)
            {
                if (bag.LootImages[i] == null)
                {
                    bag.LootImages[i] = GetComponent<SpriteRenderer>().sprite;
                    bag.CountLoot[i] = 1;
                    bag.SaveBag();
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    // Check empty place in bag
    private bool IsPlaceInBag()
    {
        BagController bag = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().Bag;
        for (int i = 0; i < bag.LootImages.Length; i++)
        {
            if (bag.LootImages[i] == null || bag.LootImages[i] == GetComponent<SpriteRenderer>().sprite)
                return true;
        }
        return false;
    }
}
