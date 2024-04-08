using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }
    public Image mask;
    float originalSize;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //originalSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }


    public void MaxSize(){
        originalSize = mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
