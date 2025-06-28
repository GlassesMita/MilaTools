# MilaTools

![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

## 项目简介

MilaTools / 米拉的工具集 是一个基于 .NET Framework 4.8 的多功能工具库，提供文件属性管理、驱动器信息获取、路径转换、注册表操作、可执行文件识别、时间戳处理、PowerShell 脚本运行等常用系统操作能力，适用于 Windows 平台的开发和自动化场景。

---

## 主要功能

- **文件属性管理**
  - 通过 `FilePropertiesEditor` 类，支持为文件添加、移除如隐藏、只读、系统等属性。

- **驱动器信息获取**
  - 通过 `GetDrivers` 类，获取所有驱动器的数量、类型、文件系统、总容量、已用/剩余容量，并可识别接口类型（如 SSD、USB）。

- **Linux 路径转 Windows 路径**
  - 通过 `ConvertLinuxPathToWindows` 类，将 Linux 风格路径（如 `~/Desktop/file.txt`）转换为 Windows 路径。

- **注册表操作**
  - 通过 `RegisterAction` 类，无需提升权限即可操作当前用户注册表，自动检测并在需要时尝试提升权限以操作 HKLM/HKR，支持注册表项的添加、修改、查询、删除。
  - 注册表数值类型通过 `RegistryValueType` 枚举定义。

- **可执行文件识别**
  - 通过 `ExecutableFileIdentification` 类，识别 Windows、Linux、macOS 等平台的可执行文件格式及其架构（如 x86、x64、ARM）。

- **系统时间与时间戳**
  - 通过 `DateTimeStamps` 类，获取本地/UTC时间，支持自定义格式输出，并可转换为 Unix/Linux 等系统的时间戳，支持时区偏移计算。

- **PowerShell 脚本运行**
  - 通过 `RunPwshCode` 类，支持直接在 C# 代码中内嵌 PowerShell 7 或 Windows PowerShell 5 脚本，并自动检测环境。

- **可执行文件十六进制处理**
  - 通过 `RunAsHexRawData` 类，将可执行文件转换为十六进制 rawData，或从 rawData 执行可执行文件。

---

## 从最新的源代码构建

1. 拉取本仓库
  ```bash
  git clone https://github.com/GlassesMita/MilaTools.git
  ```
2. 使用 Visual Studio 2022 进行构建
3. 打开项目后点击生成

---

## 使用说明

1. **添加引用**
   - 将 `MilaTools` 项目或 DLL 添加到你的解决方案中，并在代码文件顶部添加：
     ```csharp
     using MilaTools;
     ```

2. **示例：文件属性操作**
     ```csharp
     var editor = new FilePropertiesEditor();
     editor.AddProperty(@"C:\test.txt", FileProperties.Hidden);
     editor.RemoveProperty(@"C:\test.txt", FileProperties.Readonly);
     ```

3. **示例：获取驱动器信息**
     ```csharp
     var drivers = new GetDrivers();
     var details = drivers.GetAllDriveDetails();
     foreach (var d in details)
         Console.WriteLine($"{d.Name} {d.Type} {d.FileSystem} {d.TotalSize}");
     ```

4. **示例：Linux 路径转 Windows 路径**
     ```csharp
     var converter = new ConvertLinuxPathToWindows();
     string winPath = converter.Convert("~/Desktop/file.txt");
     ```

5. **示例：注册表操作**
     ```csharp
     var reg = new RegisterAction();
     reg.SetValue(Registry.CurrentUser, @"Software\MyApp", "TestValue", 123, RegistryValueType.DWord);
     var value = reg.GetValue(Registry.CurrentUser, @"Software\MyApp", "TestValue");
     reg.DeleteValue(Registry.CurrentUser, @"Software\MyApp", "TestValue");
     reg.DeleteKey(Registry.CurrentUser, @"Software\MyApp");
     ```

6. **示例：可执行文件识别**
     ```csharp
     var ident = new ExecutableFileIdentification();
     var (format, arch) = ident.Identify(@"C:\Windows\System32\notepad.exe");
     Console.WriteLine($"{format} {arch}");
     ```

7. **示例：获取时间戳**
     ```csharp
     var dt = new DateTimeStamps();
     string now = dt.GetCurrentTime("yyyy-MM-dd HH:mm:ss");
     long unix = dt.ToUnixTimestamp();
     ```

8. **示例：运行 PowerShell 脚本**
     ```csharp
     var pwsh = new RunPwshCode();
     pwsh.RunPwshWith7("Get-Process");
     pwsh.RunWithPwsh5("Get-Service");
     ```

9. **示例：字符串的转换**
    ```csharp
    string Str = @"一一四五一四";
    StringToBase64(UrlEncode(Str));
    ```
---

## TODOLIST

- [ ] 增加更多文件系统操作工具类
- [ ] 增加对 WMI 查询的异常处理和跨平台兼容性提示
- [ ] 注册表操作支持批量导入/导出
- [ ] 增加单元测试覆盖率
- [ ] 完善英文文档和 API 注释
- [ ] 添加更多的字符串转换功能
- [x] 吃一口铁托的糖果曲奇巧克力

---

## 注意事项

- 部分功能（如注册表 HKLM/HKCR 写入、驱动器接口类型识别）需要以管理员权限运行。
- 本库仅支持 Windows 平台。在 Wine 中使用可能会出现未经处理的异常。 
- 操作系统版本不应低于 Windows 7（内部版本 7601）x64。

