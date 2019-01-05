using UnityEngine;
using System.Collections;

public class EncountersInfo
{
    public int amount;
    public ENC[] encInf = new ENC[4];

    public EncountersInfo(params ENC[] encIDs)
    {
        foreach (ENC enc in encIDs)
        {
            encInf[amount] = enc;
            amount++;
        }
    }

    public Sprite[] GetEncountersPortraits()
    {
        Sprite[] temp = new Sprite[4];

        for (int i=0; i<4; i++)
        {
            temp[i] = GetPortrait(encInf[i]);
        }

        return temp;
    }

    public static Sprite GetPortrait(ENC encID)
    {
        switch (encID)
        {
            case ENC.SHADOWBEAST: return Resources.Load<Sprite>("Portraits/ShadowforgeUmbarr/Shadowbeast");
            case ENC.SOOL: return Resources.Load<Sprite>("Portraits/ShadowforgeUmbarr/SoolTheWarlock");
            case ENC.UMBARR: return Resources.Load<Sprite>("Portraits/ShadowforgeUmbarr/VoidmistressUmbarr");
            default: return Resources.Load<Sprite>("Portraits/Blackboard");
        }
    }
}
