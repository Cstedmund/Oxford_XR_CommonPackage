using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EthnicModel : System.Object {
    [SerializeField]
    private string ethnicGroup;
    [SerializeField]
    private string gender;
    [SerializeField]
    private string key;
    //[SerializeField]
    //private Sprite mapImage;
    [SerializeField]
    private Sprite previewImage;
    [SerializeField]
    private GameObject modelPrefab;
    [SerializeField]
    private Texture clothTexture;

    public EthnicModel(string ethnicGroup, string gender, string key) {
        this.ethnicGroup = ethnicGroup;
        this.gender = gender;
        this.key = key;
    }

    public string EthnicGroup {
        get { return ethnicGroup; }
    }

    public string Gender {
        get { return gender; }
    }

    public string Key {
        get { return key; }
    }

    //public Sprite MapImage {
    //    get { return mapImage; }
    //    set { mapImage = value; }
    //}

    public Sprite PreviewImage {
        get { return previewImage; }
        set { previewImage = value; }
    }

    public GameObject ModelPrefab {
        get { return modelPrefab; }
        set { modelPrefab = value; }
    }

    public Texture ClothTexture {
        get { return clothTexture; }
        set { clothTexture = value; }
    }
}
