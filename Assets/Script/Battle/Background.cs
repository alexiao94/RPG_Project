using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Background", menuName = "Background/Create new battle field")]
public class Background : ScriptableObject
{
    [SerializeField] List<Field> backgroundFields;
    [System.Serializable]

    public class Field{
        [SerializeField] string name;
        [SerializeField] Sprite background;
        public string Name {
            get{return name;}            
        }
        public Sprite Background{
            get{return background;}
        }
    }
    
    public List<Field> Fields {
    get {return backgroundFields;}
  }
}
