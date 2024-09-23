using TMPro;
using UnityEngine;

public class PassportOpen : MonoBehaviour
{
    private Passport passport;

    [SerializeField] private TextMeshProUGUI country;
    [SerializeField] private TextMeshProUGUI firstName;
    [SerializeField] private TextMeshProUGUI surname;
    [SerializeField] private TextMeshProUGUI id;
    
    private void Awake()
    {
        passport = GetComponentInParent<Passport>();
    }

    // Start is called before the first frame update
    void Start()
    {
        country.text = passport.country.name;
        firstName.text = passport.firstName;
        surname.text = passport.surname;
        id.text = passport.id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
