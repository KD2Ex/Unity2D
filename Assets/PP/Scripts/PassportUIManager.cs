using System;
using UnityEngine;
using UnityEngine.UI;

public class PassportUIManager : MonoBehaviour
{
    private Passport passport;
    
    [SerializeField] private GameObject closed;
    [SerializeField] private GameObject open;

    private Image closedImage;
    private Image openImage;
    
    private void Awake()
    {
        passport = GetComponent<Passport>();

        closedImage = closed.GetComponent<Image>();
        openImage = open.GetComponent<Image>();
    }

    private void Start()
    {
        closedImage.color = passport.country.color;
    }

    private void OnEnable()
    {
        open.SetActive(false);
        closed.SetActive(true);
    }

    public void Open()
    {
        open.SetActive(true);
        closed.SetActive(false);

        passport.open = true;
    }

    public void Close()
    {
        passport.open = false;
        
        open.SetActive(false);
        closed.SetActive(true);
    }

}
