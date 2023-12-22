using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderItem : MonoBehaviour
{
    [Header("Slider Components"), Space(5)]
    [SerializeField] private Slider _slider;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _unitPerStep;
    [SerializeField] private bool _wholeNumbers;

    [Header("Display Value"), Space(5)]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Image _border;
    [SerializeField] private bool _interactable;

    [Header("Display Value With Custom Data"), Tooltip("If you prefer using custom data rather than using number as display value, make sure you select \"Use Value As Custom Data\" box"), Space(5)]
    [SerializeField] private bool _useValueAsCustomData;
    [SerializeField] private string[] _textValues;

    private void Start()
    {
        if (_useValueAsCustomData)
        {
            _interactable = false;
            _wholeNumbers = true;
        }
        _slider.minValue = _minValue;
        _slider.maxValue = _maxValue;
        OnValueChanged(_minValue);
        _slider.wholeNumbers = _wholeNumbers;
        _slider.value = _minValue;
        _inputField.interactable = _interactable;
        _border.enabled = _interactable;
        _inputField.onSubmit.AddListener(OnSumit);
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnSumit(string arg0)
    {
        float data;
        if (float.TryParse(arg0, out data))
        {
            if (_wholeNumbers)
            {
                int value = Mathf.RoundToInt(data / _unitPerStep);
                value = (int)Math.Clamp(value, _minValue, _maxValue);
                _slider.value = value;
                _inputField.text = (value * _unitPerStep).ToString();
            }
            else
            {
                float value = data / _unitPerStep;
                value = Math.Clamp(value, _minValue, _maxValue);
                _slider.value = value;
                _inputField.text = (value * _unitPerStep).ToString();
            }
        }
        else
        {
            _inputField.text = (_slider.value * _unitPerStep).ToString();
        }
    }

    private void OnValueChanged(float arg0)
    {
        if (_useValueAsCustomData)
            _inputField.text = _textValues[Mathf.FloorToInt(arg0 - _minValue)];
        else
            _inputField.text = (arg0 * _unitPerStep).ToString();
    }

    public void SaveSetting()
    {

    }
}
