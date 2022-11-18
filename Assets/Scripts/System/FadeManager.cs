using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager:MonoBehaviour
{

    [SerializeField] Color _changedColor;

    Color _defaultColor;
    

    Image _fadeImage;

    void Start()
    {
        _defaultColor = this.gameObject.GetComponent<Color>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
