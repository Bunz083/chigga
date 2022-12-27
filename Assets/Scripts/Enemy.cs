using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    private CharacterController cc;

    public float hp = 100;
    public float maxHp = 100;

    

    void Start()
    {
        cc= GetComponent<CharacterController>();
    }

    void Update()
    {
       
        Escape();
    }

    void Chase()
    {
        Vector3 dir = transform.position = target.transform.position;
        cc.Move(dir.normalized * -5f * Time.deltaTime);
    }

    void Escape()
    {

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < 5f) 
        {
            Vector3 dir = transform.position = target.transform.position;
            cc.Move(dir.normalized * 5f * Time.deltaTime);

        }
       
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
