using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PassportData
{
    public static string[] FirstNames =
    {
        "Ida",
        "Roman",
        "David"
    };
    
    public static string[] Surnames =
    {
        "Romanovich",
        "Smith",
        "White"
    };
    
    public static string[] Ids =
    {
        "1000-1000",
        "2000-2000",
        "3000-3000",
    };
    
    public static string[] Sex =
    {
        "M",
        "F"
    };

    public static Country[] Country =
    {
        new (Color.magenta, "Kolechia"),
        new (new Color(0f, 0.27f, 0.22f), "Arstotzka"),
        new (new Color(0.5f, 0.33f, 0.04f), "Republia"),
        new (Color.red, "Orbistan"),
    };
    
    
}

[Serializable]
public class Country
{
    public Color color;
    public string name;


    public Country(Color color, string name)
    {
        this.color = color;
        this.name = name;
    }
}

public class Passport : MonoBehaviour
{
    private RectTransform rectTransform;
    
    [SerializeField] private Sprite closedImage;
    [SerializeField] private Sprite openedImage;
    
    public string firstName;
    public string surname;
    public string id;
    
    // types 
    public string sex;
    public Country country;

    public bool passed;
    public bool open;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
        GenerateData();

        Debug.Log(firstName);
        Debug.Log(surname);
        Debug.Log(id);
    }

    public void SetPass(bool value)
    {
        passed = value;
    }
    
    public void GenerateData()
    {
        var firstNameIndex = Random.Range(0, FirstNames.Length);
        var surnamesIndex = Random.Range(0, Surnames.Length);
        var idsIndex = Random.Range(0, Ids.Length);
        var countryIndex = Random.Range(0, Country.Length);
        

        firstName = FirstNames[firstNameIndex];
        surname = Surnames[surnamesIndex];
        id = Ids[idsIndex];
        country = Country[countryIndex];

    }
    
    /*
    public void OpenPassport()
    {
        Debug.Log("Open");
        isPassportOpen = true;
        
        rectTransform.sizeDelta = new Vector2(openedImage.rect.width, openedImage.rect.height);
        rectTransform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
    }

    public void ClosePassport()
    {
        Debug.Log("Close");
        isPassportOpen = false;
        
        rectTransform.sizeDelta = new Vector2(150, 200);
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
    */

    private string[] FirstNames => PassportData.FirstNames;
    private string[] Surnames => PassportData.Surnames;
    private string[] Ids => PassportData.Ids;
    private string[] Sex => PassportData.Sex;
    private Country[] Country => PassportData.Country;
}
