using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI happinessText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance == null)
            Debug.Log("Fuck");
        GameManager.Instance.changeHappiness.AddListener(updateHappiness);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateHappiness(int points)
    {
        happinessText.text = points.ToString();
    }
}
