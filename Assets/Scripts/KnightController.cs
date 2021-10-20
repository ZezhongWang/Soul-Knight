using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : Creature, BeAttack
{
    [Header("Attributes")]
    public float defense;
    public float energy;
    public float speed;
    public float coin;

    [Header("Affiliated")]
    public Slider bloodBar;
    public Slider defenseBar;
    public Slider energyBar;
    public Text coinText;
    public List<GameObject> weaponObj;
    public GameObject weaponInFloorObj;
    public int curWeapon;

    // 组件
    private Animator anim;
    private Rigidbody2D rigid;

    private Vector3 mousePosOnWorld;
    private Vector2 movement;
    private Weapon weapon;
    private bool leftMouseClick;

    void Start()
    {
        hp = 200f;
        defense = 5f;
        energy = 100f;
        bloodBar.maxValue = hp;
        bloodBar.value = hp;
        defenseBar.maxValue = defense;
        defenseBar.value = defense;
        energyBar.maxValue = energy;
        energyBar.value = energy;
        coin = 0;

        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        weapon = weaponObj[0].GetComponent<Weapon>();
        weapon.InstantiateWeapon(transform.tag);
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
            weapon.Shoot(ref energy);
            energyBar.value = energy;
        }
        if (Input.GetKeyDown(KeyCode.C))     // 按C切换武器
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
        Weapon weaponFloor = weaponInFloorObj.GetComponent<Weapon>();
        if (weaponFloor.role == "Monster") return;               // 不能捡起敌人武器
        if (weaponObj[0] != null && weaponObj[1] != null)
        {
            // 放下现有的武器
            weaponObj[curWeapon].transform.SetParent(weaponInFloorObj.transform.parent);
            weapon.PutDown();
            // 捡起新的武器
            weaponObj[curWeapon] = weaponInFloorObj;
            weapon = weaponFloor;
        }
        else
        {
            weaponObj[1] = weaponInFloorObj;
            weaponFloor.PutAway();
        }
        weaponFloor.PickUp(transform.tag);
        weaponInFloorObj.transform.SetParent(transform);
        weaponInFloorObj.transform.localPosition = new Vector3(0.13f, -0.34f, 0);
        weaponInFloorObj.transform.localRotation = Quaternion.identity;
    }

    public void BeAttack(float damage)
    {
        hp -= damage;
        bloodBar.value = hp;
        if (hp <= 0)
        {
            anim.SetBool("isDead", true);
        }
    }

    public void AddCoin()
    {
        coin += 1;
        coinText.text = coin.ToString();
    }

    public void AddMagic()
    {
        energy += 5;
        energyBar.value = energy > 100 ? 100 : energy;
    }
}
