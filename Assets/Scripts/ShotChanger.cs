using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotChanger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject[]shots;
    private MeleeDamage melee;
    void Start()
    {
        melee = GetComponent<MeleeDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackSelector(int attack)
    {
        // 1 y 6 Cyan
        // 2 y 5 Magenta
        // 3 y 4 Yellow
        switch (attack)
        {
            // DEL UNO AL TRES SERÁ UN DISPARO DISTINTO
            case 1:
                melee.GetType(DamageType.C);
                melee.Melee();
                AudioVFXController.Instance.PlayMelee();
                break;
            case 2:
                melee.GetType(DamageType.M);
                AudioVFXController.Instance.PlayMelee();
                melee.Melee();
                break;
            case 3:
                melee.GetType(DamageType.Y);
                AudioVFXController.Instance.PlayMelee();
                melee.Melee();
                break;
            case 4:
                GameObject shot4 = Instantiate(shots[0], transform.position, transform.rotation);
                AudioVFXController.Instance.PlayShot();
                break;            
            case 5:
                GameObject shot5 = Instantiate(shots[1], transform.position, transform.rotation);
                AudioVFXController.Instance.PlayShot();
                break;            
            case 6:
                GameObject shot6 = Instantiate(shots[2], transform.position, transform.rotation);
                AudioVFXController.Instance.PlayShot();
                break;
            default:
                break;
        }
    }
}
