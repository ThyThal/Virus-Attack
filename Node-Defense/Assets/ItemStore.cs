using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStore : MonoBehaviour
{
    [Header("Node Prices")]
    [SerializeField] private int node_firewall;
    [SerializeField] private int node_antivirus;
    [SerializeField] private int node_dns;
    [SerializeField] private int node_ssl;
    [SerializeField] private int node_IA;

    [Header("Buttons")]
    [SerializeField] private Text showPrice;
    [SerializeField] private Button button_firewall;
    [SerializeField] private Button button_antivirus;
    [SerializeField] private Button button_dns;
    [SerializeField] private Button button_ssl;
    [SerializeField] private Button button_ia;

    public Dictionary<string, int> items;

    // Start is called before the first frame update
    void Start()
    {
        items = new Dictionary<string, int>();

        items.Add("Firewall", node_firewall);
        items.Add("Antivirus", node_antivirus);
        items.Add("DNS", node_dns);
        items.Add("SSL", node_ssl);
        items.Add("I.A.", node_IA);

        button_firewall.onClick.AddListener(OnClickFirewall);
        button_antivirus.onClick.AddListener(OnClickAntivirus);
        button_dns.onClick.AddListener(OnClickDNS);
        button_ssl.onClick.AddListener(OnClickSLL);
        button_ia.onClick.AddListener(OnClickIA);

        void OnClickFirewall()
        {
            GetItemPrice("Firewall");
        }

        void OnClickAntivirus()
        {
            GetItemPrice("Antivirus");
        }

        void OnClickDNS()
        {
            GetItemPrice("DNS");    
        }

        void OnClickSLL()
        {
            GetItemPrice("SSL");
        }

        void OnClickIA()
        {
            GetItemPrice("I.A.");
        }
    }

    public void GetItemPrice(string itemName)
    {
        int value = items[itemName];
        showPrice.text = ("El precio del item " + itemName + " es de: " + value);
        //Debug.Log("El precio del item " + itemName + " es de: " + value);
    }
}
