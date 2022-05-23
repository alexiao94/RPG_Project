using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelector : MonoBehaviour
{
   [SerializeField] RectTransform[] selection; 
   [SerializeField] RectTransform indicator;
   [SerializeField] float moveDelay;
   int indicatorPos;

   float moveTimer; 

   void Update() {
       if (moveTimer < moveDelay) {
               moveTimer += Time.deltaTime;
           }

       if (Input.GetKey(KeyCode.UpArrow))
       {   
           if (moveTimer >= moveDelay) {
           if (indicatorPos < selection.Length - 1){
               indicatorPos++;
           }
           else {
               indicatorPos = 0;
           }
           moveTimer = 0;
           }
       }
       else if (Input.GetKey(KeyCode.DownArrow)){
           if (moveTimer >= moveDelay) {
           if (indicatorPos > 0) {
               indicatorPos--;
           }
           else{
               indicatorPos = selection.Length -1;
           }
           moveTimer = 0;
           }
       }
       indicator.localPosition = selection[indicatorPos].localPosition;

   }
}
