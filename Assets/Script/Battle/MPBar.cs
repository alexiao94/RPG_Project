using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPBar : MonoBehaviour
{
  [SerializeField] GameObject mana;


  public void SetHP(float mpNormalized) {
      mana.transform.localScale = new Vector3(mpNormalized, 1f);
  }
}
