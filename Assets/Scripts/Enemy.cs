using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public float hp = 100;
    public float maxHp = 100;

    

    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // �p�G�I���쪺�O�l�u
        if (other.tag == "Bullet")
        {
            // �����o�l�u�������O
            Bullet bullet = other.GetComponent<Bullet>();

            // ������
            hp -= bullet.atk;


            // �p�G�S��F�A�N�R���ۤv
            if (hp <= 0)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
