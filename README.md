# Soul-Knight
 Memo实习-复刻元气骑士

#### 开发日志

**10月11日**

满满的一天课...下载元气骑士玩了一大关，对基本的流程有了了解，晚上整理了需要下用到的图片素材。

**10月12日**

- 搭建了初始房间。为了重现游戏房间布局，发现有些素材还得自己在ps中修正...

- 可操纵角色Player的动画及基本行动逻辑。但素材图片不够精确，导致人物动画出现偏移，用ps调整又耗费了大量时间。
- 使用了CineMachine相机

**10月13日**

- 武器随鼠标转动
- 左键射击功能

- 滚轮切换主副武器
- 捡起地面的武器

**10月14日**

- 哥布林、野猪、哥布林祭祀三种Monster动画和动画机
- Monster的A*自动寻路
- 哥布林祭祀的AI。具有闲逛、巡逻、跟踪、攻击和死亡五种行为，当角色进入房间后，有闲逛状态变为巡逻状态；当角色进入敌人视野后，进入跟踪状态；当进入攻击距离时，发起攻击；当脱离敌人视野后，重新进入巡逻状态。

**10月15日**

- 基本的战斗系统，Monster和Player收到攻击能产生相应的反应
- 修复了能捡起怪物身上武器的bug
- UI界面——角色的血条、防御条、能量条、暂停按钮等

**10月16日**

- 手枪哥布林、弓箭哥布林和长矛哥布林三种怪的AI，以及手枪、弓箭、长矛三种武器的实现和动画
- 第一关卡地图的制作

**10月17日**

- 实现场景中可被破坏的木箱
- 实现了每个房间门的机关

**10月18日**

- 上课 + 考试......完了，感觉弄不完鸟T_T...

**10月19日**

- 多个波次敌人的生成
- 击杀房间的所有怪物后生成奖励箱子
- 击杀怪物掉落能量和金币，人物靠近可以拾取

**10月20日**

- 右键技能实现及技能条UI
- 靠近敌人时攻击方式变为手刀
- 哥布林祭祀法杖攻击实现
- 制作开始界面
- 将整个游戏连接起来
