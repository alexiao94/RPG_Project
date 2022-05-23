using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
   [SerializeField] NPCBase _base;
   [SerializeField] int Level;
   [SerializeField] bool isPlayer;

   public NPC npc {get; set;}
   
   Image image;
   Vector3 originalPos;
   Color originalColor;
   AudioClip sound;
   private void Awake(){
       image = GetComponent<Image> ();
       originalPos = image.transform.localPosition;
       originalColor = image.color;
   }

   public void Setup() {
       npc = new NPC(_base,Level);
       GetComponent <Image> ().sprite = npc.Base.Sprite;
       image.color = originalColor;
       PlayEnterAnimation();


   }

   public void PlayEnterAnimation() {
       if (isPlayer) {
           image.transform.localPosition = new Vector3(-500f, originalPos.y);
       }
       else{
           image.transform.localPosition = new Vector3(500f, originalPos.y);
       }
       image.transform.DOLocalMoveX(originalPos.x, 2.5f);
   }

   public void PlayAttackAnimation() {
       var sequence = DOTween.Sequence();
       if (isPlayer) {
           sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 250, 1f));
       }
       else {
           sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 250, 1f));
       }
       sequence.Append(image.transform.DOLocalMoveX(originalPos.x, 1f));
   }

   public void PlayHitAnimation(){
       var sequence = DOTween.Sequence();

       sequence.Append(image.DOColor(Color.gray,0.1f));

       sequence.Append(image.DOColor(originalColor,0.1f));

   }

   public void PlayDeadAnimation(){
       var sequence = DOTween.Sequence();
       sequence.Append(image.DOFade(0f,0.5f));
    
   }

}
