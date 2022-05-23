using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleDialogBox : MonoBehaviour
{   
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] Text dialogText;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject ActionSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] GameObject moveBox;
    [SerializeField] GameObject status;

    [SerializeField] List<Text> actionTexts; 
    [SerializeField] List<Text> moveTexts; 
    
    [SerializeField] Text moveDetailsText;
    int damage;
    public void SetDialog(string dialog){
        dialogText.text = dialog;
    }
    public IEnumerator TypeDialog(string dialog) {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
        yield return new WaitForSeconds(1f);
    }

    public void EnableMoveBox(bool enabled) {
        moveBox.SetActive(enabled);
    }
    public void EnableStatus(bool enabled) {
        status.SetActive(enabled);
    }
    public void UpdateActionSelection (int selectedAction) {
        for(int i=0; i<actionTexts.Count;++i){
            if(i == selectedAction) {
                actionTexts[i].color = highlightedColor;
            }
            else {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection (int selectedMove, List<Move> moves) {
        for(int i=0; i<moveTexts.Count;++i){
            if(i == selectedMove) {
                moveTexts[i].color = highlightedColor;
            }
            else {
                moveTexts[i].color = Color.black;
            }
        }
        damage = moves[selectedMove].Base.PSTR + moves[selectedMove].Base.PINT + moves[selectedMove].Base.PDEX;
        moveDetailsText.text = $"{moves[selectedMove].Base.Description} \n Power: {damage}";
    }


    public void SetMoveNames(List<Move> moves){
        for(int i=0; i<moveTexts.Count; ++i) {
            if (i < moves.Count){
                moveTexts[i].text = moves[i].Base.Name;
            }
            else {
                moveTexts[i].text = "-";
            }
        }
    }

}
