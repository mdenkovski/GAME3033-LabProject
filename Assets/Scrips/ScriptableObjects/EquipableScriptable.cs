using System.Collections;
using System.Collections.Generic;

public abstract class EquipableScriptable : ItemScriptable
{
    public bool Equipped { get => m_Equipped; set { m_Equipped = value; } }
    private bool m_Equipped;
    public override void UseItem(PlayerController controller)
    {
        m_Equipped = !m_Equipped;

    }

}
