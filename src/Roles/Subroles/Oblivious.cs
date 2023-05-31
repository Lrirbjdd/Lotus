using Lotus.Roles.Internals;
using Lotus.Roles.Internals.Attributes;
using UnityEngine;

namespace Lotus.Roles.Subroles;

public class Oblivious: Subrole
{
    public override string Identifier() => "⁈";

    [RoleAction(RoleActionType.SelfReportBody)]
    private void CancelReportBody(ActionHandle handle) => handle.Cancel();

    protected override RoleModifier Modify(RoleModifier roleModifier) => base.Modify(roleModifier).RoleColor(new Color(0.49f, 0.28f, 0.5f));
}