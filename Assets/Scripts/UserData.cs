using UnityEngine;
using UnityEngine.Events;

public interface IUserDataBonusReadOnly
{
    public int SnowflakeBonusAmount { get; }
    public int SteamBonusAmount { get; }

    public event UnityAction<IUserDataBonusReadOnly> AmountChanged;
}

public sealed class UserData : MonoBehaviour, IUserDataBonusReadOnly
{
    private const int BonusMaxAmount = 15;

    [SerializeField] private int[] _bonuses = new int[2];

    public event UnityAction<IUserDataBonusReadOnly> AmountChanged;

    public int SnowflakeBonusAmount => _bonuses[0];
    public int SteamBonusAmount => _bonuses[1];

    public void AddItem(ItemType itemType)
    {
        _bonuses[(int)itemType]++;
        _bonuses[(int)itemType] = Mathf.Clamp(_bonuses[(int)itemType], 0, BonusMaxAmount);

        AmountChanged?.Invoke(this);
    }

    public bool TryUseBonus(ItemType itemType)
    {
        if (_bonuses[(int)itemType] > 0)
        {
            _bonuses[(int)itemType]--;
            AmountChanged?.Invoke(this);
            return true;
        }

        return false;
    }
}
