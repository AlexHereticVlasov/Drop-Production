using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _button;
    
    private int _index;
    private UnityAction<int> _load;
    
    public void Init(int index, UnityAction<int> action)
    {
        _load = action;
        _index = index;
        _text.text = (index + 1).ToString();
        _button.onClick.AddListener(() => Launch());
    }

    private void Launch() => _load.Invoke(_index);
}
