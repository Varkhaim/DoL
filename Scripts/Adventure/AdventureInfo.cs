using UnityEngine;
using System.Collections;

public class AdventureInfo
{
    public string MissionTitle;
    public string MissionDescription;
    public AdventureRequirements Requirements;
    public EncountersInfo encountersInfo;

    public AdventureInfo(string _title, string _desc, AdventureRequirements _req, EncountersInfo _encs)
    {
        MissionTitle = _title;
        MissionDescription = _desc;
        Requirements = _req;
        encountersInfo = _encs;
    }
}
