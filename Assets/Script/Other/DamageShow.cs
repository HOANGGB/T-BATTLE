using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageShow : MonoBehaviour
{
    public int damageS;
    [SerializeField] TextMeshProUGUI textDamage;
    
    void Update()
    {
        // transform.LookAt(Camera.main.transform);
    }
    private void OnEnable() {
        textDamage.text = damageS.ToString();
    }

    public void Off(){
        gameObject.SetActive(false);
    }
}
