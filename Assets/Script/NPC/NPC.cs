using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC
{
    public NPCBase Base {get; set;}
    public int level {get; set;}

    public int HP {get; set;}
    public int MP {get; set;}

    public List<Move> Moves {get; set;}

    public NPC(NPCBase pBase, int pLevel)
    {
        Base = pBase;
        level = pLevel;
        HP = MaxHp;
        MP = MaxMp;

        //Generate moves
        Moves =  new List<Move>();
        foreach (var move in Base.LearnableMoves) {
            if (move.Level <= level) {
                Moves.Add(new Move(move.Base));
            }
        }
    }
    public int STR {
        get {return Mathf.FloorToInt((Base.STR * level)/100f) + 5;}
    }
    public int CON {
        get {return Mathf.FloorToInt((Base.CON * level)/100f) + 5;}
    }
    public int INT {
        get {return Mathf.FloorToInt((Base.INT * level)/100f) + 5;}
    }
    public int AGI {
        get {return Mathf.FloorToInt((Base.AGI * level)/100f) + 5;}
    }
    public int DEX {
        get {return Mathf.FloorToInt((Base.DEX * level)/100f) + 5;}
    }
    public int LUK {
        get {return Mathf.FloorToInt((Base.LUK * level)/100f) + 5;}
    }
    public int MaxHp {
        get {return Mathf.FloorToInt((Base.MaxHp * level)/100f) + 10;}
    }
      public int MaxMp {
        get {return Mathf.FloorToInt((Base.MaxMp * level)/100f) + 10;}
    }
}
