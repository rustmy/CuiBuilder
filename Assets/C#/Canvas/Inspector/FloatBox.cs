﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FloatBox : MonoBehaviour {
    [Serializable]
    public class OnFloatBoxChangedEvent : UnityEvent<object>
    { }

    [SerializeField] private InputField m_InputField;

    public OnFloatBoxChangedEvent OnEndEdit;
    public OnFloatBoxChangedEvent OnValueChanged;

    private void Awake()
    {
        EnableEvents();
    }

    public object Get()
    {
        float num;
        if (!float.TryParse(m_InputField.text, out num))
        {
            return num;
        }
        return m_InputField.text;
    }

    public void Set(string value)
    {
        DisableEvents();
        m_InputField.text = value;
        EnableEvents();
    }

    private void DisableEvents()
    {
        m_InputField.onEndEdit.RemoveAllListeners();
        m_InputField.onValueChanged.RemoveAllListeners();
    }

    private void EnableEvents()
    {
        m_InputField.onEndEdit.AddListener((s) => SendEventCallback(OnEndEdit));
        m_InputField.onValueChanged.AddListener((s) => SendEventCallback(OnEndEdit));
    }

    public void SendEventCallback(OnFloatBoxChangedEvent callback)
    {
        if (callback == null) return;
        callback.Invoke(Get());
    }
}
