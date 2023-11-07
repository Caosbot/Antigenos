using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public string sceneName;
    [SerializeField] private GameObject _itemPrefab;
    private TextMeshProUGUI textInfo;
    [SerializeField] private Transform _itemContent;
    
    
    
    public void LoadScene()
    {
        SceneManager.LoadScene("1.0_Phase");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Start()
    {
        GameObject gameItem = Instantiate<GameObject>(_itemPrefab, _itemContent);
        Button button = gameItem.GetComponent<Button>();
        if (button == null)
        {
            Debug.Log("Vazio");
        }
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PULMÂO";

    }
}
