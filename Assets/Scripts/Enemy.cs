using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public Transform firepoint;
    public GameObject bulletPrefab;
    private CharacterController cc;

    public float hp = 100;
    public float maxHp = 100;

    

    void Start()
    {
        cc= GetComponent<CharacterController>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > 5f)
        {
            Vector3 dir = transform.position = target.transform.position;
            cc.Move(dir.normalized * -5f * Time.deltaTime);

        }



    }

    

    
    private void OnTriggerEnter(Collider other)
    {
        // 如果碰撞到的是子彈
        if (other.tag == "Bullet")
        {
            // 先取得子彈的攻擊力
            Bullet bullet = other.GetComponent<Bullet>();

            // 先扣血
            hp -= bullet.atk;


            // 如果沒血了，就刪除自己
            if (hp <= 0)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
