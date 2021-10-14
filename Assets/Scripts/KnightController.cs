using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;

    [Header("Affiliated")]
    public List<GameObject> weaponObj;
    public GameObject weaponInFloorObj;
    public int curWeapon;

    private Vector2 movement;
    private Animator anim;
    private Rigidbody2D rigid;
    private Vector3 mousePosOnWorld;
    private Weapon weapon;
    private bool leftMouseClick;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        weapon = weaponObj[0].GetComponent<Weapon>();
        curWeapon = 0; 
    }

  
    void Update()
    {
        Move();

        // 检测身边是否有枪
        weaponInFloorObj = null;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Weapon"))
            {
                weaponInFloorObj = cols[i].gameObject;
                break;
            }
        }
        leftMouseClick = Input.GetMouseButtonDown(0); 
        if (weaponInFloorObj != null && leftMouseClick)
        {
            GetWeapon();
        }
        if (Input.GetMouseButton(0) && weaponInFloorObj == null)
        {
            weapon.Shoot();
        }
        if (Input.GetMouseButtonDown(2))     // 滚轮被点击
        {
            SwitchWeapon();
        }
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + movement * speed * Time.fixedDeltaTime);
    }

    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement == new Vector2(0, 0))
            anim.SetBool("isRun", false);
        else
            anim.SetBool("isRun", true);
        // Player Direction
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosOnScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
        mousePosOnWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        GetComponent<SpriteRenderer>().flipX = transform.position.x >= mousePosOnWorld.x;
        // weapon Direction
        weapon.LookAt(mousePosOnWorld);
    }

    // 切换主副武器
    void SwitchWeapon()
    {
        if (weaponObj[1] == null) return;
        switch(curWeapon) {
            case 0:
                curWeapon = 1;
                weaponObj[0].GetComponent<Weapon>().PutAway();
                weaponObj[1].GetComponent<Weapon>().TakeOut();
                break;
            case 1:
                curWeapon = 0;
                weaponObj[0].GetComponent<Weapon>().TakeOut();
                weaponObj[1].GetComponent<Weapon>().PutAway();
                break;
        }
        weapon = weaponObj[curWeapon].GetComponent<Weapon>();
    }

    // 捡起地面的武器
    void GetWeapon()      
    {
        if (weaponObj[0] != null && weaponObj[1] != null)
        {
            // 放下现有的武器
            weaponObj[curWeapon].transform.SetParent(weaponInFloorObj.transform.parent);
            weapon.PutDown();
            // 捡起新的武器
            weaponObj[curWeapon] = weaponInFloorObj;
            weapon = weaponInFloorObj.GetComponent<Weapon>();
        }
        else
        {
            weaponObj[1] = weaponInFloorObj;
            weaponObj[1].GetComponent<Weapon>().PutAway();
        }
        weaponInFloorObj.GetComponent<Weapon>().PickUp();
        weaponInFloorObj.transform.SetParent(transform);
        weaponInFloorObj.transform.localPosition = new Vector3(0.13f, -0.34f, 0);
        weaponInFloorObj.transform.localRotation = Quaternion.identity;
    }
}
