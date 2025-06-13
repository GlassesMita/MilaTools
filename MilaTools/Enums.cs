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