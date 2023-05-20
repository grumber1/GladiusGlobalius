using System.Collections.Generic;
using UnityEngine;

public static class GladiatorNameGenerator
{
    public static string[] vornamen;
    public static string[] nachnamen;

    public static string generateGladiatorName()
    {
        int randomNumber = Methods.randomNumber(0, vornamen.Length - 1);
        string gladiatorVorname = vornamen[randomNumber];

        randomNumber = Methods.randomNumber(0, nachnamen.Length - 1);
        string gladiatorNachname = nachnamen[randomNumber];
        string gladiatorFullName = (gladiatorVorname + " " + gladiatorNachname).Replace("\n", "");
        Debug.Log("gladiatorFullName: " + gladiatorFullName);

        return gladiatorFullName;
    }

    public static List<string> generateGladiatorNames(int count)
    {
        List<string> gladiatorNames = new List<string>();

        for (int i = 0; i < count; i++)
        {
            gladiatorNames.Add(generateGladiatorName());
        }

        return gladiatorNames;
    }
}
