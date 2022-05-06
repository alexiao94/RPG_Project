using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleUnit : MonoBehaviour
{
   [SerializeField] NPCBase _base;
   [SerializeField] int Level;
   [SerializeField] bool isPlayer;

   public NPC npc {get; set;}

   public void Setup() {
       npc = new NPC(_base,Level);
       GetComponent <Image> ().sprite = npc.Base.Sprite;

   }
}
