using System;

[Serializable]
public class SettingData
{
    #region JoyStick settings
    private float _joyStickAlpha;
    public float JoyStickAlpha { get { return _joyStickAlpha; } set { _joyStickAlpha = Math.Clamp(value, 0f, 1f); } }

    private bool _joyStickInteractable;
    public bool JoyStickInteractable { get { return _joyStickInteractable; } set { _joyStickInteractable = value; } }

    private bool _joyStickFixPosition;
    public bool JoyStickFixPosition { get { return _joyStickFixPosition; } set { _joyStickFixPosition = value; } }

    private float _joyStickScale;
    public float JoyStickScale { get { return _joyStickScale; } set { _joyStickScale = Math.Clamp(value, 0.5f, 2f); } }
    #endregion

    # region Sound settings
    private float _SFXVolume;
    public float SFXVolume { get { return _SFXVolume; } set { _SFXVolume = Math.Clamp(value, 0f, 1f); } }

    private float _BGMVolumn;
    public float BGMVolume { get { return _BGMVolumn; } set { _BGMVolumn = Math.Clamp(value, 0f, 1f); } }

    private bool _SFXMute;
    public bool SFXMute { get { return _SFXMute; } set { _SFXMute = value; } }

    private bool _BGMMute;
    public bool BGMMute { get { return _BGMMute; } set { _BGMMute = value; } }

    private bool _muteAll;
    public bool MuteAll { get { return _muteAll; } set { _muteAll = value; } }
    #endregion

    public void Init()
    {
        _joyStickAlpha = 1f;
        _joyStickInteractable = true;
        _joyStickFixPosition = false;
        _joyStickScale = 1f;

        _SFXVolume = 1f;
        _BGMVolumn = 1f;
        _SFXMute = false;
        _BGMMute = false;
        _muteAll = false;
    }
}
