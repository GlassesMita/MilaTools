/// <summary>
/// File properties for use with FileAction.
/// </summary>
public enum FileProperties
{
    Hidden,
    System,
    Archive,
    Readonly
}

/// <summary>
/// Registry value types for use with RegisterAction.
/// </summary>
public enum RegistryValueType
{
    String,
    ExpandString,
    DWord,
    QWord,
    Binary,
    MultiString
}


/// <summary>
/// Taskbar progress states for use with TaskbarProgress.
/// </summary>
public enum TaskbarProgressState
{
    NoProgress = 0,
    Indeterminate = 0x1,
    Normal = 0x2,
    Error = 0x4,
    Paused = 0x8
}

/// <summary>
/// Logger levels for use with Logger.Log() .
/// </summary>
public enum LogLevel
{
    Debug = 0,
    Info = 1,
    Warning = 2,
    Error = 3,
    Fatal = 4
}


/*
                     ,----------------,              ,---------,
                ,-----------------------,          ,"        ,"|
              ,"                      ,"|        ,"        ,"  |
             +-----------------------+  |      ,"        ,"    |
             |  .-----------------.  |  |     +---------+      |
             |  |                 |  |  |     | -==----'|      |
             |  |  I LOVE DOS!    |  |  |     |         |      |
             |  |  Bad command or |  |  |/----|`---=    |      |
             |  |  PS C:\> _      |  |  |   ,/|==== ooo |      ;
             |  |                 |  |  |  // |(((( [33]|    ,"
             |  `-----------------'  |," .;'| |((((     |  ,"
             +-----------------------+  ;;  | |         |,"
                /_)______________(_/  //'   | +---------+
           ___________________________/___  `,
          /  oooooooooooooooo  .o.  oooo /,   \,"-----------
         / ==ooooooooooooooo==.o.  ooo= //   ,`\--{)B     ,"
        /_==__==========__==_ooo__ooo=_/'   /___________,"

        */