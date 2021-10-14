using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    [Header("Attributes")]
    public float speed;

    [Header("Affiliated")]
    public GameObject MainWeaponObj;
    public GameObject SecWeaponObj;
    public GameObject CurWeaponObj;
    public GameObject WeaponInFloorObj;

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
        CurWeaponObj = MainWeaponObj;
        weapon = CurWeaponObj.GetComponent<Weapon>();
    }

  
    void Update()
    {
        Move();
        
        if(Input.GetMouseButton(0))
        {
            weapon.Shoot();
        }
        if (Input.GetMouseButtonDown(2))     // 滚轮被点击
        {
            SwitchWeapon();
        }
        leftMouseClick = Input.GetMouseButtonDown(0);

        // 检测身边是否有枪
        WeaponInFloorObj = null;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Weapon"))
            {
                WeaponInFloorObj = cols[i].gameObject;
                break;
            }
        }
        if(WeaponInFloorObj != null && leftMouseClick)
        {
            GetWeapon();
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

    void SwitchWeapon()    // 切换主副武器
    {
        if (SecWeaponObj == null) return;
        if (CurWeaponObj == MainWeaponObj)
        {
            CurWeaponObj = SecWeaponObj;
            MainWeaponObj.GetComponent<Weapon>().PutAway();
            SecWeaponObj.GetComponent<Weapon>().TakeOut();
        }
        else
        {
            CurWeaponObj = MainWeaponObj;
            MainWeaponObj.GetComponent<Weapon>().TakeOut();
            SecWeaponObj.GetComponent<Weapon>().PutAway();
        }
        weapon = CurWeaponObj.GetComponent<Weapon>();
    }

    void GetWeapon()      // 捡起地面的武器
    {
        if (MainWeaponObj != null && SecWeaponObj != null)
        {
            CurWeaponObj.transform.SetParent(WeaponInFloorObj.transform.parent);
            weapon.PutDown();
        }
        else CurWeaponObj = SecWeaponObj;       // 这里有问题，用bool再试下吧
        CurWeaponObj = WeaponInFloorObj;
        weapon = CurWeaponObj.GetComponent<Weapon>();
        weapon.PickUp();
        CurWeaponObj.transform.SetParent(transform);
        CurWeaponObj.transform.localPosition = new Vector3(0.13f, -0.34f, 0);
        CurWeaponObj.transform.localRotation = Quaternion.identity;
    }
}
