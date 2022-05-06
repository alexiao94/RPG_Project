using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "NPC/Create new Move")]
public class MoveBase : ScriptableObject
{
  [SerializeField] string name;

  [TextArea]
  [SerializeField] string description;

  [SerializeField] int pSTR;
  [SerializeField] int pINT;
  [SerializeField] int pDEX;
  [SerializeField] int accuracy;
  [SerializeField] int mpCost;
  [SerializeField] int hpCost;

  public string Name {
      get {return name;}
  }
  public string Description {
      get {return description;}
  }
  public int PSTR {
      get {return pSTR;}
  }
  public int PINT {
      get {return pINT;}
  }
  public int PDEX {
      get {return pDEX;}
  }
  public int Accuracy {
      get {return accuracy;}
  }
  public int MpCost {
      get {return mpCost;}
  }
  public int HpCost {
      get {return hpCost;}
  }
}
