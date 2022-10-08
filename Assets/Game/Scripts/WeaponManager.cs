using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;

    [SerializeField] private Transform[] weaponSlots;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject currentMainWeapon = Instantiate(weapons[0], weaponSlots[0].position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
