using UnityEngine;
using UnityEngine.UI;

public class BagController : MonoBehaviour
{
    [SerializeField] private GameObject[] _imageBoxes = new GameObject[3];
    [SerializeField] private Text[] _textBoxes = new Text[3];
    [SerializeField] private GameObject[] _deleteButtons = new GameObject[3];

    public Sprite[] LootImages = new Sprite[3];
    public int[] CountLoot = new int[3];

    private void Start()
    {
        LoadBag();
    }

    // Update visual info about bag
    private void FixedUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            _imageBoxes[i].GetComponent<Image>().sprite = LootImages[i];
            if (_imageBoxes[i].GetComponent<Image>().sprite == null)
                _imageBoxes[i].GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            else
                _imageBoxes[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            if (CountLoot[i] > 1)
                _textBoxes[i].text = CountLoot[i].ToString();
            else
                _textBoxes[i].text = "";
        }
    }

    // Send new info in dataFile
    public void SaveBag()
    {
        DataManager.SaveBag(this);
    }

    // Load saved info from dataFile
    public void LoadBag()
    {
        BagData data = DataManager.LoadBag();

        if (data != null)
        {
            LootImages = data.LootImages;
            CountLoot = data.CountLoot;
        }
    }

    // Open button delete loot
    public void ClickCell(int cellNumber)
    {
        if (_imageBoxes[cellNumber].GetComponent<Image>().sprite != null)
            if (!_deleteButtons[cellNumber].activeSelf)
                _deleteButtons[cellNumber].SetActive(true);
            else
                _deleteButtons[cellNumber].SetActive(false);
    }

    // Cleen cell in bag
    public void DeleteLoot(int cellNumber)
    {
        LootImages[cellNumber] = null;
        CountLoot[cellNumber] = 0;
        SaveBag();
        _deleteButtons[cellNumber].SetActive(false);
    }
}
