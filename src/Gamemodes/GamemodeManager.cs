using System;
using System.Collections.Generic;
using System.Linq;
using TownOfHost.Gamemodes.CaptureTheFlag;
using TownOfHost.Gamemodes.Colorwars;
using TownOfHost.Gamemodes.Debug;
using TownOfHost.Gamemodes.FFA;
using TownOfHost.Gamemodes.Standard;
using TownOfHost.Options;
using VentLib.Logging;

namespace TownOfHost.Gamemodes;

// As we move to the future we're going to try to use instances for managers rather than making everything static
public class GamemodeManager
{
    public List<IGamemode> Gamemodes = new();

    public IGamemode CurrentGamemode
    {
        get => _currentGamemode;
        set
        {
            _currentGamemode?.InternalDeactivate();
            _currentGamemode = value;
            _currentGamemode?.InternalActivate();
        }
    }

    private IGamemode _currentGamemode;
    public OptionHolder GamemodeOption;
    internal readonly List<Type> GamemodeTypes = new() { typeof(StandardGamemode), typeof(TestHnsGamemode), typeof(ColorwarsGamemode), typeof(DebugGamemode), typeof(CTFGamemode)};

    public void SetGamemode(int id)
    {
        CurrentGamemode = Gamemodes[id];
        VentLogger.Old($"Setting Gamemode {CurrentGamemode.GetName()}", "Gamemode");
    }

    public void Setup()
    {
        Gamemodes = GamemodeTypes.Select(g => (IGamemode)g.GetConstructor(Array.Empty<Type>())!.Invoke(null)).ToList();

        SmartOptionBuilder builder = new SmartOptionBuilder()
            .Name("Gamemode")
            .IsHeader(true)
            .BindInt(SetGamemode);

        for (int i = 0; i < Gamemodes.Count; i++)
        {
            IGamemode gamemode = Gamemodes[i];
            var index = i;
            builder.AddValue(v => v.Text(gamemode.GetName()).Value(index).Build());
        }

        GamemodeOption = builder.Build();
        TOHPlugin.OptionManager.AllHolders.Insert(0, GamemodeOption);
        GamemodeOption.valueHolder.UpdateBinding();
    }
}