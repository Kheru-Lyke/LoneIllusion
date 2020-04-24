using Com.SchizophreniaStudios.LoneIllusionDestiny.Common;
using Com.SchizophreniaStudios.LoneIllusionDestiny.LoneIllusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    BLUNT,
    CUNNING,
    NOBLE,
    TRUE
}

[CreateAssetMenu(fileName = "ChangeStatBy", menuName ="Visual Novel/Movement/Change Player Stat")]
public class ChangePlayerStat : CharacterMovement
{
    [SerializeField] private Stats statToChange = Stats.BLUNT;
    [SerializeField] private int value = 0;

    public override void Move()
    {
        switch (statToChange)
        {
            case Stats.BLUNT:
                PlayerCharacter.blunt += value;
                break;
            case Stats.CUNNING:
                PlayerCharacter.cunning += value;
                break;
            case Stats.NOBLE:
                PlayerCharacter.noble += value;
                break;
            case Stats.TRUE:
                PlayerCharacter.lTrue += value;
                break;
        }


    }
}
