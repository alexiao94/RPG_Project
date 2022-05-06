using System.IO.Pipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;
    [SerializeField] MPBar mpBar;

    public void SetData (NPC npc) {
        nameText.text = npc.Base.Name + "\t" + "Lvl " + npc.level;
        // levelText.text =  "Lvl " + npc.level;
        hpBar.SetHP((float)npc.HP/npc.MaxHp);
        mpBar.SetHP((float)npc.MP/npc.MaxMp);
    }
}
