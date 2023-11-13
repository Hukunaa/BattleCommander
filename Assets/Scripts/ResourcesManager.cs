using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private ResourcesManager() { }
    public static ResourcesManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(ResourcesManager)) as ResourcesManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private Dictionary<string, Mesh> _meshes = new Dictionary<string, Mesh>();

    private static ResourcesManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        instance = this;
    }

    private void Start()
    {
        Object[] allMeshes = Resources.LoadAll("Models", typeof(GameObject));
        foreach (Object m in allMeshes)
        {
            //Need to instantiate the object to access its components as its a prefab by default
            GameObject castGameObject = Instantiate((GameObject)m);
            castGameObject.SetActive(false);
            _meshes.Add(castGameObject.GetComponent<MeshFilter>().mesh.name, castGameObject.GetComponent<MeshFilter>().mesh);
        }
    }
    public Dictionary<string, Mesh> Meshes { get => _meshes; }
}
