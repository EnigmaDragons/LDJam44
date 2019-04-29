using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleAndNameScript : MonoBehaviour
{


    public Text nameText;
    public Text roleText;
    int x = 0;
    int y = 0;

    public void ChangeName()
    {
        if (x == AllCredits.Length)
        {
            nameText.text = "";
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
            roleText.text = "";
        }
        else
        {
            roleText.text = AllCredits[y].Role;
            y++;
        }
    }


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
    public static CreditData LeadArtist = new CreditData
    {
        //RoleColor = Color.
        Role = "Lead Artist",
        Name = "Shadow"
    };
    public static CreditData LeadMusician = new CreditData
    {
        //RoleColor = Color.
        Role = "Lead Musician",
        Name = "Bradley Seymour"
    };
    public static CreditData LeadStorymaker = new CreditData
    {
        //RoleColor = Color.
        Role = "Lead Storymaker",
        Name = "Playfulwarrior"
    };
    public static CreditData[] AllCredits = { LeadProgrammer, LeadDesigner, EzraProgrammer, LeadArtist, LeadMusician, LeadStorymaker};
}

public class CreditData
{
    //public Color RoleColor { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
}