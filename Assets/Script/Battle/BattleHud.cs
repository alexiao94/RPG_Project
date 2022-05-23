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
    NPC _npc;
    public void SetData (NPC npc) {

        _npc = npc;

        nameText.text = npc.Base.Name;
        levelText.text =  "Lvl " + npc.level;
        hpBar.SetHP((float)npc.HP/npc.MaxHp);
        mpBar.SetHP((float)npc.MP/npc.MaxMp);
    }

    public IEnumerator UpdateHP(){
        yield return hpBar.SetHPSmooth((float)_npc.HP/_npc.MaxHp);
    }
}
