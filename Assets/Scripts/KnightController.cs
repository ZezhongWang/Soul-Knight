using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnightController : Creature, BeAttack
{
    public static KnightController Instance { get; private set; }
    public Room room { get; set; }

    [Header("Attributes")]
    public float defense;
    public float energy;
    public float speed;
    public float skillVal;
    public float skillSustainTime;
    public float skillRecoverCD;
    public float coin;

    [Header("Affiliated")]
    public Slider bloodBar;
    public Slider defenseBar;
    public Slider energyBar;
    public Text coinText;
    public Slider skillBar;
    public List<GameObject> weaponObj;
    public List<Weapon> weapons;
    public GameObject weaponInFloorObj;
    public int curWeapon;

    // 组件
    private Animator anim;
    private Animator skillAnim;
    private Rigidbody2D rigid;

    private Vector3 mousePosOnWorld;
    private Vector2 movement;
    private Weapon weapon;
    private Weapon skillWeapon;
    private Weapon handWeapon;
    private bool leftMouseClick;
    private bool skillState;
    private float skillRecoverTimeStamp;
    private bool monsterNear;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        hp = 200f;
        defense = 5f;
        energy = 100f;
        skillVal = 100f;
        skillRecoverCD = 0.2f;
        skillSustainTime = 5f;
        bloodBar.maxValue = hp;
        bloodBar.value = hp;
        defenseBar.maxValue = defense;
        defenseBar.value = defense;
        energyBar.maxValue = energy;
        energyBar.value = energy;
        coin = 0;
        skillBar.maxValue = skillVal;
        skillBar.value = skillVal;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        weapon = weaponObj[0].GetComponent<Weapon>();
        weapon.InstantiateWeapon(transform.tag);
        handWeapon = weaponObj[2].GetComponent<Weapon>();
        handWeapon.InstantiateWeapon(transform.tag);
        curWeapon = 0;
        skillAnim = transform.Find("Skill").gameObject.GetComponent<Animator>();
        skillWeapon = null;
    }

  
    void Update()
    {
        if (hp <= 0) return;

        Move();

        UpdateSKillVal();

        // 检测身边是否有枪和怪物
        monsterNear = false;
        weaponInFloorObj = null;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Weapon") && cols[i].gameObject.GetComponent<Weapon>().role != "Monster")
            {
                if(weaponInFloorObj == null) weaponInFloorObj = cols[i].gameObject;
            }
            if(cols[i].CompareTag("Monster"))
            {
                monsterNear = true;
            }
        }
        // 技能恢复
        if (Time.time - skillRecoverTimeStamp >= skillRecoverCD)
        {
            skillRecoverTimeStamp = Time.time;
            skillVal++;
            if (skillVal > 100)
                skillVal = 100;
        }

        leftMouseClick = Input.GetMouseButtonDown(0); 

        if (weaponInFloorObj != null && leftMouseClick)
        {
            GetWeapon();
        }
        if ((Input.GetKeyDown(KeyCode.J) || Input.GetMouseButton(0)) && weaponInFloorObj == null)
        {
            if(!monsterNear)
            {
                weapon.Shoot(ref energy);
                if (skillWeapon) skillWeapon.Shoot(ref energy);
            }
            else handWeapon.Shoot(ref energy);
            energyBar.value = energy;

        }
        if (Input.GetKeyDown(KeyCode.C))     // 按C切换武器
        {
            if (skillState) returnOrdinary();
            SwitchWeapon();
        }
        if(Input.GetMouseButtonDown(1) && skillVal == 100)
        {
            skill();
            Invoke("returnOrdinary", skillSustainTime);
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

        if (room != null)
        {
            Transform monsterTrans = room.GetNearestMonster();
            //Vector3 monsterPos = monsterTrans == null? rigid
            if (monsterTrans != null)
            {
                weapon.LookAt(monsterTrans.position);
                handWeapon.LookAt(monsterTrans.position);
            }
        }
        
        //Vector3 mousePosOnScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
        //mousePosOnWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        //GetComponent<SpriteRenderer>().flipX = transform.position.x >= mousePosOnWorld.x;
        //// weapon Direction
        //weapon.LookAt(mousePosOnWorld);
        //handWeapon.LookAt(mousePosOnWorld);
        //if (skillWeapon) skillWeapon.LookAt(mousePosOnWorld);
    }

    // 切换主副武器
    void SwitchWeapon()
    {
        curWeapon = (curWeapon + 1) % weapons.Count;
        SwitchToWeapon(curWeapon);

        //if (weaponObj[1] == null) return;
        //switch (curWeapon)
        //{
        //    case 0:
        //        curWeapon = 1;
        //        weaponObj[0].GetComponent<Weapon>().PutAway();
        //        weaponObj[1].GetComponent<Weapon>().TakeOut();
        //        break;
        //    case 1:
        //        curWeapon = 0;
        //        weaponObj[0].GetComponent<Weapon>().TakeOut();
        //        weaponObj[1].GetComponent<Weapon>().PutAway();
        //        break;
        //}
        //weapon = weaponObj[curWeapon].GetComponent<Weapon>();
    }

    void SwitchToWeapon(int index)
    {
        Destroy(weapon.gameObject);
        weapon = Instantiate(weapons[index], transform);
        weapon.PickUp(transform.tag);
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(-0.1f, -0.34f, 0);
        weapon.transform.localRotation = Quaternion.identity;
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
        weaponInFloorObj.transform.localPosition = new Vector3(-0.1f, -0.34f, 0);
        weaponInFloorObj.transform.localRotation = Quaternion.identity;
    }

    public void BeAttack(float damage)
    {
        hp -= damage;
        bloodBar.value = hp;
        if (hp <= 0)
        {
            anim.SetBool("isDead", true);
            Invoke("ReturnToMainMenu", 2f);
        }
    }

    public void skill()
    {
        skillVal = 0;
        skillState = true;
        skillAnim.Play("Fire");
        GameObject skillWeaponObj = Instantiate(weaponObj[curWeapon], transform.position, Quaternion.identity);
        skillWeaponObj.transform.SetParent(transform);
        skillWeaponObj.transform.localPosition = new Vector3(0.5f, -0.34f, 0);
        skillWeapon = skillWeaponObj.GetComponent<Weapon>();
    }

    public void returnOrdinary()
    {
        if(skillState)
        {
            skillState = false;
            skillAnim.Play("Idle");
            Destroy(skillWeapon.gameObject);
            skillWeapon = null;
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
        if (energy > 100) energy = 100;
        energyBar.value = energy;
    }

    public void AddHp()
    {
        hp += 5;
        if (hp > 200) hp = 200;
        bloodBar.value = hp;
    }

    public void UpdateSKillVal()
    {
        skillBar.value = skillVal;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
