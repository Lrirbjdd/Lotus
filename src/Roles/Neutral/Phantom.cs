using TownOfHost.Options;
using TownOfHost.Roles.Internals;
using TownOfHost.Roles.Internals.Attributes;

namespace TownOfHost.Roles;

public class Phantom : Crewmate
{
    private int phantomClickAmt;
    private int phantomAlertAmt;
    public bool CanKill;
    public bool IsAlerted;
    protected override RoleModifier Modify(RoleModifier roleModifier)
    {
        return roleModifier
        .RoleColor("#662962")
        .SpecialType(SpecialType.Neutral);
    }

    [RoleAction(RoleActionType.RoundStart)]
    public void Reset()
    {
        CanKill = false;
        IsAlerted = false;
    }

    // I ASSUME TO MAKE IT NOT KILL THEM YOU GOT TO PUT IT IN EVERY ROLE FILE BUT IM TOO LAZY 4 THAT
    [RoleAction(RoleActionType.MyDeath)]
    public void PhantomDeath()
    {
        if (!CanKill)
        {
            // TODO: make it do something lol
        }
    }

    public override bool CanBeKilled() => CanKill;

    protected override SmartOptionBuilder RegisterOptions(SmartOptionBuilder optionStream) =>
         base.RegisterOptions(optionStream)
             .Tab(DefaultTabs.NeutralTab)
             .AddSubOption(opt =>
                opt.Name("Tasks Remaining for Phantom Click")
                .BindInt(v => phantomClickAmt = v)
                .AddIntRangeValues(1, 10, 1)
                .Build())
            .AddSubOption(opt =>
                opt.Name("Tasks Remaining for Phantom Alert")
                .BindInt(v => phantomAlertAmt = v)
                .AddIntRangeValues(1, 5, 1)
                .Build());
}