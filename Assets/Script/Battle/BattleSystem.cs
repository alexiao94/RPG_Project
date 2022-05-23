using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {Start, PlayerAction, PlayerMove, EnemyMove, Busy}
public class BattleSystem : MonoBehaviour
{
  [SerializeField] BattleUnit playerUnit;
  [SerializeField] BattleHud playerHud;
  [SerializeField] BattleUnit enemyUnit;
  [SerializeField] BattleHud enemyHud;
  [SerializeField] BattleDialogBox dialogBox;
   

  public event Action<bool> OnBattleOver;

  BattleState state;
  [SerializeField] float moveDelay;
  [SerializeField] RectTransform[] selectionAction;
  [SerializeField] RectTransform[] selectionMoves;
  [SerializeField] RectTransform indicatorAction;
  [SerializeField] RectTransform indicatorMoves;
  [SerializeField] GameObject pointerAction;
  [SerializeField] GameObject pointerMove;
  int indicatorActionPos;
  int indicatorMovePos;
  int currentAction;
  int currentMove;
  float moveTimer; 

  public void StartBattle(){
      StartCoroutine(SetupBattle());
  }

  public IEnumerator SetupBattle() {
      playerUnit.Setup();
      playerHud.SetData(playerUnit.npc);
      enemyUnit.Setup();
      enemyHud.SetData(enemyUnit.npc);
      dialogBox.SetMoveNames(playerUnit.npc.Moves);
    
      yield return dialogBox.TypeDialog($"A {enemyUnit.npc.Base.Name} has appeared!");
      PlayerAction();
      yield return new WaitForSeconds(0.1f);
      pointerAction.SetActive(true);

  }

  void PlayerAction(){
    state = BattleState.PlayerAction;
    dialogBox.EnableStatus(true);
  }
  void PlayerMove() {
    state = BattleState.PlayerMove;
    dialogBox.EnableMoveBox(true);
    dialogBox.EnableStatus(false);
  }
  void PlayerRun(){
    float random = UnityEngine.Random.Range(0,100f);
    UnityEngine.Debug.Log(random);
    StartCoroutine(PlayerRunHandle(random));
  }
  IEnumerator PlayerRunHandle(float random) {
    state = BattleState.Busy;
    yield return dialogBox.TypeDialog("Running...");
    if ( random < 40f){
      yield return dialogBox.TypeDialog("Succesfully escaped!");
      yield return new WaitForSeconds(1f);
      OnBattleOver(true);
    }
    else {
      yield return dialogBox.TypeDialog($"{enemyUnit.npc.Base.Name} blocked you");
      StartCoroutine(EnemyMove());
    }
  }
  IEnumerator PerformPlayerMove() {
    state = BattleState.Busy;
    var move = playerUnit.npc.Moves[currentMove];
    yield return dialogBox.TypeDialog($"{playerUnit.npc.Base.Name} used {move.Base.Name}");
    playerUnit.PlayAttackAnimation();
    yield return new WaitForSeconds(1f);
    enemyUnit.PlayHitAnimation();
    var damageDetails = enemyUnit.npc.TakeDamage(move,playerUnit.npc);
    yield return enemyHud.UpdateHP();
    yield return ShowDamageDetails(damageDetails);
    yield return dialogBox.TypeDialog($"{playerUnit.npc.Base.Name} dealt {damageDetails.Damage} damage");
    

    if (damageDetails.Fainted) {
    enemyUnit.PlayDeadAnimation();
    yield return dialogBox.TypeDialog($"{enemyUnit.npc.Base.Name} defeated.");
    yield return new WaitForSeconds(2f);
    OnBattleOver(true);
    }
    else{
      StartCoroutine(EnemyMove());
    }
  }
  IEnumerator EnemyMove() {
    state = BattleState.EnemyMove;
    var move = enemyUnit.npc.GetRandomMove();

    yield return dialogBox.TypeDialog($"{enemyUnit.npc.Base.Name} used {move.Base.Name}");
    enemyUnit.PlayAttackAnimation();
    yield return new WaitForSeconds(1f);
    playerUnit.PlayHitAnimation();
    var damageDetails = playerUnit.npc.TakeDamage(move,enemyUnit.npc);
    yield return playerHud.UpdateHP();
    yield return ShowDamageDetails(damageDetails);
    yield return dialogBox.TypeDialog($"{enemyUnit.npc.Base.Name} dealt {damageDetails.Damage} damage");

    if (damageDetails.Fainted) {
    playerUnit.PlayDeadAnimation();
    yield return dialogBox.TypeDialog($"{playerUnit.npc.Base.Name} defeated.");
    yield return new WaitForSeconds(2f);
    OnBattleOver(false);
    }
    else{
      PlayerAction();
    }
  }
  IEnumerator ShowDamageDetails (DamageDetails damageDetails){
    if (damageDetails.Critical > 1f){
      yield return dialogBox.TypeDialog("Critical hit!");
    }
  }
  public void HandleUpdate() {
    if (state == BattleState.PlayerAction){
      HandleActionSelection();
    }
    else if (state == BattleState.PlayerMove) {
      HandleMoveSelection();
    }
  }

  void HandleActionSelection() {
         if (moveTimer < moveDelay) {
               moveTimer += Time.deltaTime;
           }

       if (Input.GetKeyDown(KeyCode.UpArrow))
       {   
           if (moveTimer >= moveDelay) {
            if (indicatorActionPos < selectionAction.Length - 1){
                indicatorActionPos++;
                
            }
            else {
                indicatorActionPos = 0;
            }
            moveTimer = 0;
            
            if(currentAction < 1 ){
              ++currentAction;
            }
            else {
              currentAction = 0;
            }
          }
       }
       else if (Input.GetKeyDown(KeyCode.DownArrow)){
          if (moveTimer >= moveDelay) {
            if (indicatorActionPos > 0) {
                indicatorActionPos--;
            }
            else{
                indicatorActionPos = selectionAction.Length -1;
            }
            moveTimer = 0;
            
            if(currentAction > 0 ){
              --currentAction;
            }
            else {
              currentAction = 1;
            }
          }
       }
       else if(Input.GetKeyDown(KeyCode.Z)) {
         if (currentAction == 0){
           PlayerMove();
         }
         else if (currentAction == 1){
           PlayerRun();
         }
       }
      indicatorAction.localPosition = selectionAction[indicatorActionPos].localPosition;
      dialogBox.UpdateActionSelection(currentAction);
   }

   void HandleMoveSelection(){
     if (moveTimer < moveDelay) {
               moveTimer += Time.deltaTime;
           }
        
       if (Input.GetKeyDown(KeyCode.UpArrow))
       {   
           if (moveTimer >= moveDelay) {
            if (indicatorMovePos > 1){
                indicatorMovePos-=2;
                
            }
            else {
                if(indicatorMovePos %2 != 0){
                  indicatorMovePos +=  1;
                }
                else {
                indicatorMovePos = selectionMoves.Length - 1;
                currentMove = indicatorMovePos;
                }
            }
            moveTimer = 0;
      
          }
       }
       else if (Input.GetKeyDown(KeyCode.DownArrow)){
          if (moveTimer >= moveDelay) {

            if (indicatorMovePos < selectionMoves.Length - 2) {
                indicatorMovePos += 2;
            }
            else{
                if(indicatorMovePos%2 == 0) {
                  indicatorMovePos -= 1;
                }
                else {
                indicatorMovePos = 0;
                }
            }
            moveTimer = 0;

          }
       }
      else if (Input.GetKeyDown(KeyCode.RightArrow)){
           if (moveTimer >= moveDelay) {
            if (indicatorMovePos < selectionMoves.Length - 1){
                indicatorMovePos++;
                
            }
            else {
                indicatorMovePos = 0;
            }
            moveTimer = 0;
            
          }
       }
      else if (Input.GetKeyDown(KeyCode.LeftArrow)){
          if (moveTimer >= moveDelay) {
            if (indicatorMovePos > 0) {
                indicatorMovePos--;
            }
            else{
                indicatorMovePos = selectionMoves.Length -1;
            }
            moveTimer = 0;
          }
       }
     
      currentMove = indicatorMovePos;
      indicatorMoves.localPosition = selectionMoves[indicatorMovePos].localPosition;
      dialogBox.UpdateMoveSelection(currentMove, playerUnit.npc.Moves);
    
      if (Input.GetKeyDown(KeyCode.Z)) {
         dialogBox.EnableMoveBox(false);
         dialogBox.EnableStatus(true);

         StartCoroutine(PerformPlayerMove());
       }
      if (Input.GetKeyDown(KeyCode.X)) {
         dialogBox.EnableMoveBox(false);
         dialogBox.EnableStatus(true);
         PlayerAction();
       }

   }
}
