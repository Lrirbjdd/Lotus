using System.Linq;
using HarmonyLib;
using Lotus.API.Odyssey;
using Lotus.Roles.Internals;
using Lotus.Roles.Internals.Attributes;
using Lotus.Extensions;
using VentLib.Logging;
using VentLib.Utilities.Optionals;

namespace Lotus.Patches.Actions;

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
class TaskCompletePatch
{
    public static void Prefix(PlayerControl __instance, uint idx)
    {
        VentLogger.Info($"TaskComplete:{__instance.GetNameWithRole()}", "CompleteTask");

        NormalPlayerTask? task = __instance.myTasks.ToArray().FirstOrDefault(t => t.Id == idx) as NormalPlayerTask;
        
        ActionHandle handle = ActionHandle.NoInit();
        Game.TriggerForAll(RoleActionType.TaskComplete, ref handle, __instance, Optional<NormalPlayerTask>.Of(task));
    }
}