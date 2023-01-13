using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EthnicDB", menuName = "Custom Scriptable Object/Create Ethnic DB")]
public class ScriptableEthnicGroup : ScriptableObject {
    public List<EthnicModel> list;
}