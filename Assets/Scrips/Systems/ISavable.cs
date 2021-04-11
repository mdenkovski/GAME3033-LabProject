using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class SaveDataBase
{
    public string Name;
}
public interface ISavable
{
    SaveDataBase SaveData();
    void LoadData(SaveDataBase saveData);
}
