public enum ElementState
{
    Covered,
    Uncovered,
    Marked
}

public enum ElementType
{
    SingleCovered,
    DoubleCovered,
    CantCovered
}

public enum ElementContent
{
    Number,
    Trap,
    Tool,
    Gold,
    Enemy,
    Door,
    BigWall,
    SmallWall,
    Exit
}

public enum ToolType
{
    Hp,
    Armor,// 护甲
    Sword,// 剑
    Arrow,// 箭
    Key,
    Tnt,// 炸药
    Hoe,// 锄头
    Grass,// 幸运草
    Map
}

public enum GoldType
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven
}