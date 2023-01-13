using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthnicDatabase : MonoBehaviour {
    //private static EthnicDatabase instance;
    //public static EthnicDatabase Instance {
    //    get { return instance; }
    //}

    //private void Awake() {
    //    if (instance != null && instance != this) {
    //        Destroy(this.gameObject);
    //    } else {
    //        instance = this;
    //    }
    //}


    public ScriptableEthnicGroup ethnicGroups;

    private void Start() {
        Debug.Log(ethnicGroups.list.Count);
    }

    private EthnicModel selectedEthnicGroup;
    public EthnicModel SelectedEthnicGroup {
        get { return selectedEthnicGroup; }
        set { selectedEthnicGroup = value; }
    }

    public List<EthnicModel> GetAllEthnicGroups() {
        return ethnicGroups.list;
    }

    public EthnicModel GetEthnicByIndex(int index) {
        if(index < 0 || index >= ethnicGroups.list.Count) { return null; }

        return ethnicGroups.list[index];
    }
}