using UnityEngine;


public static class SaveSystemSimple
{
    private static int localeID;
    // Start is called before the first frame update
    private static int genderID; // 0 - female, 1 - male

    private static string nameCharacter;
    public static void SetLocaleID(int newLocaleID)
    {
        // обов'язково збережемо
        PlayerPrefs.SetInt("localeID", newLocaleID);
        localeID = newLocaleID;
    }


    public static int GetLocaleID()
    {
        localeID = PlayerPrefs.GetInt("localeID");
        return localeID;
    }

    public static void SetGanderID(int newGenderID)
    {
        PlayerPrefs.SetInt("genderID", newGenderID);
        genderID = newGenderID;
    }

    public static int GetGenderID()
    {
        genderID = PlayerPrefs.GetInt("genderID");
        return genderID;
    }

    public static void SetNameCharacter(string name)
    {
        PlayerPrefs.SetString("nameCharacter", name);
        nameCharacter = name;
    }

    public static string GetNameCharacter()
    {
        nameCharacter = PlayerPrefs.GetString("nameCharacter");
        return nameCharacter;
    }
}
