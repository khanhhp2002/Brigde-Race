using UnityEngine;

public class SettingManager : Singleton<SettingManager>
{
    public SettingData SettingData;

    public void LoadSetting()
    {
        if (SettingData == null)
        {
            SettingData = new SettingData();
            SettingData.Init();
            Debug.Log(SettingData.JoyStickAlpha);
            bool isLoaded = SaveManager.Instance.LoadData(ref SettingData, "SettingData");
            if (!isLoaded)
            {
                Debug.Log("Save new setting");
                SaveManager.Instance.SaveData(SettingData, "SettingData");
            }
        }
    }
    public void SaveSetting()
    {
        SaveManager.Instance.SaveData(SettingData, "SettingData");
    }
}
