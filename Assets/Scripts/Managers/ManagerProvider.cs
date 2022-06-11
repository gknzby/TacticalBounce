using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerProvider
{
    private static ManagerProvider Instance;
    private List<IManager> Managers;

    private ManagerProvider()
    {
        Managers = new List<IManager>();
    }
    ~ManagerProvider()
    {
        if(Managers != null)
            Managers.Clear();
    }
    private static ManagerProvider GetInstance()
    {
        if(Instance == null)
        {
            Instance = new ManagerProvider();
        }
        return Instance;
    }

    public static void AddManager(IManager manager)
    {
        GetInstance().Managers.Add(manager);
    }

    public static void RemoveManager(IManager manager)
    {
        GetInstance().Managers.Remove(manager);
    }

    public static IManager GetManager(string ManagerType)
    {
        ManagerProvider mP = GetInstance();

        foreach(IManager manager in mP.Managers)
        {
            if(manager.ManagerType == ManagerType)
                return manager;
        }

        Debug.LogError("There is no " + ManagerType + " in the Scene, add an " + ManagerType + " to the Scene");
        return null;
    }

}
