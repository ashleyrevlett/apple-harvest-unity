using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPigScore : MonoBehaviour
{
    public GameObject pigObject;
    Pig pig;
    TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        pig = pigObject.GetComponent<Pig>();
        scoreText = gameObject.GetComponent<TMP_Text>();
        scoreText.text = "Pig: 0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Pig: {pig.score}";
    }
}
