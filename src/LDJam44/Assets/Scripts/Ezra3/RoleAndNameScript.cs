using UnityEngine;
using UnityEngine.UI;

public class RoleAndNameScript : MonoBehaviour
{
    public Text nameText;
    public Text roleText;
    public GameObject RoleAndName;
    int x = 0;
    int y = 0;

    public void ChangeName()
    {
        if (x == AllCredits.Length)
        {
            RoleAndName.SetActive(false);
        }
        else
        {
            nameText.text = AllCredits[x].Name;
            x++;
        }
    }
    public void ChangeRole()
    {
        if (y == AllCredits.Length)
        {
            RoleAndName.SetActive(false);
        }
        else
        {
            roleText.text = AllCredits[y].Role;
            y++;
        }
    }

    public static CreditData ProducedBy = new CreditData
    {
        //RoleColor = Color.
        Role = "Produced By",
        Name = "Enigma Dragons"
    };

    public static CreditData Director = new CreditData
    {
        //RoleColor = Color.
        Role = "Game Director",
        Name = "Silas Reinagel"
    };

    public static CreditData ProjectManager = new CreditData
    {
        //RoleColor = Color.
        Role = "Project Manager",
        Name = "Silas Reinagel"
    };

    public static CreditData LeadProgrammer = new CreditData
    {
        //RoleColor = Color.
        Role = "Lead Programmer",
        Name = "Silas Reinagel"
    };

    public static CreditData LeadDesigner = new CreditData
    {
        //RoleColor = Color.
        Role = "Lead Designer",
        Name = "Noah Reinagel"
    };

    public static CreditData EzraProgrammer = new CreditData
    {
        //RoleColor = Color.
        Role = "Programmer",
        Name = "Ezra3"
    };

    public static CreditData Animator = new CreditData
    {
        //RoleColor = Color.
        Role = "Animations",
        Name = "Ezra3"
    };

    public static CreditData UI = new CreditData
    {
        Role = "User Interface",
        Name = "Noah Reinagel"
    };

    public static CreditData LeadArtist = new CreditData
    {
        //RoleColor = Color.
        Role = "Lead Artist",
        Name = "Shaddo"
    };
    public static CreditData SoundEffects = new CreditData
    {
        //RoleColor = Color.
        Role = "Sound Effects",
        Name = "Bradley Seymour"
    };
    public static CreditData MusicDirection = new CreditData
    {
        //RoleColor = Color.
        Role = "Music Direction",
        Name = "Silas Reinagel"
    };

    public static CreditData SettingAndStory = new CreditData
    {
        //RoleColor = Color.
        Role = "Lead Storymaker",
        Name = "Cynthia Reinagel"
    };

    public static CreditData Tester = new CreditData
    {
        //RoleColor = Color.
        Role = "Quality Assurance",
        Name = "Gordy Keene"
    };

    public static CreditData[] AllCredits = 
    {
        ProducedBy,

        Director,
        LeadDesigner,
        LeadArtist,

        LeadProgrammer,
        EzraProgrammer,
        UI,

        Tester,
        MusicDirection,
        ProjectManager,
        SoundEffects,
        SettingAndStory,
        Animator
    };
}

public class CreditData
{
    //public Color RoleColor { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
}