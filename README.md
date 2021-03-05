# UntieUnite
Reverse Engineering of Pokémon UNITE

![License](https://img.shields.io/badge/License-GPLv3-blue.svg)

### Usage:

- Obtain a built version of the UntieUnite.ConsoleApp project's executable.
  - This `.sln` can be built with `.NET Core 3.1` by using `dotnet build untieunite.sln` via command prompt.
- Create a `.bat` file next to the executable:
  - `untieunite.consoleapp {dump}\assets\DlcRoot\0.3.0\DLC_0 {out_dir}`
  - Replace `{dump}` with the folder that contains your download data from the app (having the above path).
  - Replace `{out_dir}` with the directory you want the dumps to be placed in.
- Run your `.bat` file, and the contents will be dumped for further analysis.

When the Console project is run the program will decrypt, decompress, and export as much content as possible.

### Notes:

- Pokémon UNITE is a Unity 5x game developed by TiMi Studios (Tencent), with various reverse engineering deterrents.
- Game code has some obfuscations, similar to those done to League of Legends: Wild Rift ([details about that game's obfuscation here](https://katyscode.wordpress.com/2021/01/15/reverse-engineering-adventures-league-of-legends-wild-rift-il2cpp/))
- Bundles have some XOR encryption on the metadata to break the usual asset viewing programs.
- `LanguageMap` contains most of the localization text.
- `Lua` scripts are compiled (version 5.3).

### Other Useful Programs:

- [Il2CppInspector](https://github.com/djkaty/Il2CppInspector) -- useful if you want to reverse engineer the game code.
- [AssetStudio](https://github.com/Perfare/AssetStudio) -- can open the dumped `.bundle` files to view &amp; export assets.

### Credits:

By [SciresM](https://github.com/SciresM/) &amp; [Kaphotics](https://github.com/kwsch/)
