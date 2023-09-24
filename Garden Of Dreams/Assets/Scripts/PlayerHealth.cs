using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _fullHealth, _currentHealth;
    [SerializeField] private GameObject _healthSlider;

    private void FixedUpdate()
    {
        // Displaying the current health on the strip
        _healthSlider.GetComponent<Slider>().value = _currentHealth / _fullHealth;

        // Death at 0 health
        if (_currentHealth <= 0)
            Death();
    }

    // At death, it resets the accumulated loot and reloads the scene
    private void Death()
    {
        BagController bag = GetComponent<PlayerMove>().Bag;
        for (int i = 0; i < bag.LootImages.Length; i++)
        {
                bag.LootImages[i] = null;
                bag.CountLoot[i] = 0;
        }
        bag.SaveBag();
        SceneManager.LoadScene("SampleScene");
    }

    // Taking damage from enemies
    public void GetDamage(float damage)
    {
        _currentHealth -= damage;
    }
}
