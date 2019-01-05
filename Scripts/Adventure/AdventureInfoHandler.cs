using UnityEngine;
using System.Collections;

public class AdventureInfoHandler
{
    public AdventureInfo[] adventureInfo = new AdventureInfo[10];
    private int advCounter = 0;

    public AdventureInfoHandler()
    {
        AddNewAdventure("Whispers of Despair",
            "At Southeast's Manor dark clouds have gathered a while ago. Creepy purple light emanates from the building. Rumours say, that mysterious hooded figure appeared in front of their house and devoured souls of habitants. Though there was no sign of life from manor since then no one tried to check what happened. It is time to check what happened...",
            new AdventureRequirements(2, 1, 2),
            new EncountersInfo(ENC.ATLANTA, ENC.MRS_SOUTHEAST, ENC.MR_SOUTHEAST, ENC.DEMON)
            );
        AddNewAdventure("Shadowforge - Umbarr",
            "Dark forces try to corrupt these lands with their void energy. Shadowforges gather nearby sadness and will to disappear to reforge it into fuel that allows shadowbeasts to persist during daytime. Your spy found out that this Shadowforge is led by Voidmistress Umbarr and her siblings. Let's break this family reunion.",
            new AdventureRequirements(1),
            new EncountersInfo(ENC.SHADOWBEAST, ENC.SOOL, ENC.UMBARR)
            );
        AddNewAdventure("Eugene the Witch",
            "Eugene was a village hero once. Everytime bandits were here to loot whole village he was the one in charge to defend against invaders. He was really brave warrior, but unfortunately... too reckless. When he was going to take off witch living in the nearby woods he wasn't aware of her magic abilities. While he has defeated her she was able to use her spirit to haunt him to take control over his body. Combined Eugene's rage and witch's hatred to humankind caused body to twist into abberation which only goal is to spread destruction. You have to stop him... her... them?",
            new AdventureRequirements(1, 1),
            new EncountersInfo(ENC.MRS_SOUTHEAST)
            );
    }

    private void AddNewAdventure(string _title, string _desc, AdventureRequirements _req, EncountersInfo _enc)
    {
        adventureInfo[advCounter] = new AdventureInfo(_title, _desc, _req, _enc);
        advCounter++;
    }

    public AdventureInfo GetInfo(int _advID)
    {
        if (_advID > 2) _advID = 2;
        return adventureInfo[_advID];
    }

    public Encounter GetEncounter(Adventure _adv, int _diff)
    {
        return Encounter.GetBossAI(adventureInfo[_adv.ID].encountersInfo.encInf[_adv.progress]);
    }
}
