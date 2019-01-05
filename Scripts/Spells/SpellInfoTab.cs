/*using UnityEngine;
using System.Collections;

public class SpellInfoTab
{
    
    public SpellInfo[] spellInfo = new SpellInfo[50];

    private const float CD_MOD = 1;
    private const float CAST_MOD = 1;
    private const float MULTIP_MOD = 1;
    private const float MANA_MOD = 0.8f;

    public SpellInfoTab()
    {
        
    }

    private void AddSpellInfo(SPELL _ID, string _name, int _basevalue, float _coeff, int _basevalue2, float _coeff2, int _castTime, int _cooldown, int _charges, int _chargesGain, string _icon, int _HoTgap, int _ticks, int _manaCost, HEALTYPE _healtype, bool _channeling=false)
    {
        spellInfo[(int)_ID] =
            new SpellInfo(
                (int)_ID,
                _name,
                (int)(_basevalue * MULTIP_MOD),
                _coeff * MULTIP_MOD,
                (int)(_basevalue2 * MULTIP_MOD),
                _coeff2 * MULTIP_MOD,
                (int)(_castTime * CAST_MOD),
                (int)(_cooldown * CD_MOD),
                _charges,
                _chargesGain,
                _icon,
                _HoTgap,
                _ticks,
                (int)(_manaCost * MANA_MOD),
                _healtype,
                _channeling
                );
    }
}
*/