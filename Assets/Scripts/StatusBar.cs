using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Enemy target;
    public Image HpBar;
    public Text HpNumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {


            Vector3 offset = new Vector3(0, 25, 0);
            Vector3 p = Camera.main.WorldToScreenPoint(target.transform.position);
            transform.position = p + offset;

            float sx = target.hp / target.maxHp;
            if (sx < 0)
            {
                sx = 0;
            }

            HpBar.rectTransform.localScale = new Vector3(sx, 1, 1);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }


    }
}
