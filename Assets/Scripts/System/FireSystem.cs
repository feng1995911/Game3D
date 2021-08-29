using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FireSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject M_PrefabFire;
    
    private static FireSystem M_Instance;
    public static FireSystem Instance
    {
        get { return M_Instance; }
    }

    private void Awake()
    {
        M_Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FireElement CreateFireElement(Transform transformParent)
    {
        Debug.Assert(M_PrefabFire == null, "火焰系统没有设置火焰元素预制体!");
        GameObject fire = Instantiate(M_PrefabFire, transformParent);
        fire.transform.localPosition = new Vector3(UnityEngine.Random.Range(-1, 1.2f), UnityEngine.Random.Range(0.3f, 0.6f), UnityEngine.Random.Range(-0.8f, 0.7f));
        return fire.GetComponent<FireElement>();
    }
    
    public bool RemoveFireElement(Transform transform)
    {
        Destroy(transform.gameObject);
        return true;
    }
}
