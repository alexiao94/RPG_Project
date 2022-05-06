using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/Create new NPC")]
public class NPCBase : ScriptableObject
{
  [SerializeField] string name;
  [TextArea]
  [SerializeField] string description;

  [SerializeField] Sprite sprite;
  [SerializeField] Racetype type1;
  [SerializeField] Racetype type2;
  
  //Base Stats
  [SerializeField] int _maxHp;
  [SerializeField] int _maxMp;
  [SerializeField] int _str;
  [SerializeField] int _con;
  [SerializeField] int _int;
  [SerializeField] int _agi;
  [SerializeField] int _dex;
  [SerializeField] int _luk;
  
  [SerializeField] List<LearnableMove> learnableMoves;

  [System.Serializable]
  public class LearnableMove
  {
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;
    public MoveBase Base {
      get {return moveBase;}
    }
    public int Level {
      get {return level;}
    }
  }
  public enum Racetype{
    None,
    Humanoid,
    Beast,
    Bug,
    Fish,
    Undead,
    Mecha,
    Holy,
    Elemental
  }
  public string Name {
    get {return name;}
  }
  public string Description {
    get {return description;}
  }
  public Sprite Sprite {
    get {return sprite;}
  }
  public Racetype Type1 {
    get {return type1;}
  }
  public Racetype Type2 {
    get {return type2;}
  }
  public int MaxHp {
    get {return _maxHp;}
  }
  public int MaxMp {
    get {return _maxMp;}
  }
  public int STR {
    get {return _str;}
  }
  public int CON {
    get {return _con;}
  }
  public int INT {
    get {return _int;}
  }
  public int AGI {
    get {return _agi;}
  }
  public int DEX {
    get {return _dex;}
  }
  public int LUK{
    get {return _luk;}
  }
  public List<LearnableMove> LearnableMoves {
    get {return learnableMoves;}
  }
}

