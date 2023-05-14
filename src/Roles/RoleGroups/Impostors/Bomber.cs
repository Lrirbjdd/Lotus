
using VentLib.Logging;

namespace Lotus.Roles.RoleGroups.Impostors;

public class Bomber : Vanilla.Impostor
{
    protected override RoleModifier Modify(RoleModifier roleModifier)
    {
        base.Modify(roleModifier);
        VentLogger.Warn($"{this.RoleName} Not Implemented Yet", "RoleImplementation");
        return roleModifier;
    }
}