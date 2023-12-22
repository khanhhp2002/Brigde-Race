using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] List<Material> materials;
    [SerializeField] private List<ColorType> colorTypes = new List<ColorType>() { ColorType.Red, ColorType.Orange, ColorType.Blue, ColorType.Yellow };

    public List<ColorType> GetColorTypes() => colorTypes;
    public Material GetMaterialByColorType(ColorType colorType) => materials[(int)colorType];

    private void Start()
    {
        SettingManager.Instance.LoadSetting();
    }
}
