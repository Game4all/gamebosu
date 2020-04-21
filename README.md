<div align="center">
<h1><code>gamebosu</code></h1>
A ruleset that adds a playable gameboy to osu!lazer.
<br/>
</div>

# Building the ruleset.

Assuming you've got the .NET Core SDK tools in your path

```bash
    cd osu.Game.Rulesets.Gamebosu
    dotnet build -c:Release # make sure to build ruleset in release mode to create a single file assembly
    # You should find the output assembly in osu.Game.Rulesets.Gamebosu/bin/Release/osu.Game.Rulesets.Gamebosu.Packed.dll
```

For building this from an IDE, you should open the solution file with your prefered C# editor and hit `build` with the `Release` configuration

# Installation 

- Go to osu!lazer executable folder (`%localappdata%/osulazer` on windows)
- Drag and drop the file previously built or prebuilt there.
- Launch lazer. You should see the ruleset.

# Installing Roms

This ruleset basically ports a gameboy emulator to lazer, so you'll need gameboy (color) rom files to play it.

- Launch lazer with the ruleset installed and launch a beatmap (this is to create the roms folder).
- Go to osu!lazer data folder (`%appdata%/osu` on windows)
- Drag your gameboy (color) roms into `osu/roms`


# How to use

- Please make sure you've applied steps from **Installing Roms**
- Launch lazer and start a beatmap; the game selection screen should appear
- Use your configured gameboy DPAD left and right keys to choose the Rom.
- Hit your configured gameboy A, START or SELECT key to launch the emulator.
- Enjoy :)


# Acknowledgements

The emulator uses [Emux](https://github.com/Washi1337/Emux) by _Washi1337_ as its emulation core.

Original idea of running a gameboy emulator on o!f : [osu-GameBoy](https://github.com/osu-Karaoke/osu-GameBoy)