using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    [SerializeField] Text _countText;
    void Start()
    {
        StartCoroutine(Counting());
    }

    IEnumerator Counting()
    {
        _countText.text = "3";

        yield return new WaitForSeconds(1.0f);

        _countText.text = "2";

        yield return new WaitForSeconds(1.0f);

        _countText.text = "1";

        yield return new WaitForSeconds(1.0f);

        _countText.text = "GO";

        yield return new WaitForSeconds(1.0f);

        _countText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
