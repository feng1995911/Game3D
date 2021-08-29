using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WoodElement : MonoBehaviour
{
    [SerializeField]
    private Collider M_Collider;
    private List<FireElement> M_SelfFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;
        if (layer.Equals(9))
        {
            OnFire(other.GetComponent<FireElement>());
        }
        else if (layer.Equals(4))
        {
            OnWater(other.GetComponent<WaterElement>());
        }
    }

    private void OnFire(FireElement fire)
    {
//        gameObject.transform.localScale = Vector3.one * 0.7f;
        for (int i = 0; i <= 3; i++)
        {
            CreateFire(fire);
        }
    }

    private void OnWater(WaterElement water)
    {
//        gameObject.transform.localScale = Vector3.one;
        for (int i = 0; i <= 5; i++)
        {
            RemoveFire(water);
        }
    }

    private void CreateFire(FireElement fire)
    {
        if (M_SelfFire == null)
        {
            M_SelfFire = new List<FireElement>();
        }
//        else if (M_SelfFire.Contains(fire))
//        {
//            return; // 自己的火不能烧自己;
//        }
    
        if (M_SelfFire.Count >= 7) // 火点满了
        {
            return;
        }
        
        M_SelfFire.Add(FireSystem.Instance.CreateFireElement(transform));
    }

    private void RemoveFire(WaterElement water)
    {
        if (M_SelfFire == null || M_SelfFire.Count <= 0)
        {
            return;
        }

        FireElement fire = M_SelfFire[M_SelfFire.Count - 1];
        if (FireSystem.Instance.RemoveFireElement(fire.transform))
        {
            M_SelfFire.RemoveAt(M_SelfFire.Count - 1);
        }
    }
}
