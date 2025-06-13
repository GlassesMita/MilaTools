using System;
using System.Diagnostics;
using System.IO;

namespace MilaTools
{
    /// <summary>
    /// Provides methods to run PowerShell (pwsh) code and check for pwsh 7 installation.
    /// </summary>
    public class RunPwshCode
    {
        /// <summary>
        /// Runs a sample PowerShell 7 script directly in this method.
        /// </summary>
        public void RunPwsh()
        {
            string script = "Write-Output 'Hello from PowerShell 7'";
            RunPwshWith7(script);
        }

        /// <summary>
        /// Runs the given PowerShell 7 script if pwsh 7 is installed, otherwise throws an exception.
        /// </summary>
        /// <param name="script">PowerShell script to execute.</param>
        public void RunPwshWith7(string script)
        {
            string pwshPath = "pwsh";
            try
            {
                // Check if pwsh 7 is installed by running 'pwsh -v'
                ProcessStartInfo checkInfo = new ProcessStartInfo
                {
                    FileName = pwshPath,
                    Arguments = "-v",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var checkProcess = Process.Start(checkInfo))
                {
                    string output = checkProcess.StandardOutput.ReadToEnd();
                    string error = checkProcess.StandardError.ReadToEnd();
                    checkProcess.WaitForExit();

                    if (checkProcess.ExitCode != 0 || !output.StartsWith("PowerShell"))
                        throw new InvalidOperationException("PowerShell 7 (pwsh) is not installed or not available in PATH.");
                }

                // Run the actual script
                string tempScript = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".ps1");
                File.WriteAllText(tempScript, script);

                ProcessStartInfo runInfo = new ProcessStartInfo
                {
                    FileName = pwshPath,
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{tempScript}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var runProcess = Process.Start(runInfo))
                {
                    string result = runProcess.StandardOutput.ReadToEnd();
                    string error = runProcess.StandardError.ReadToEnd();
                    runProcess.WaitForExit();

                    if (runProcess.ExitCode != 0)
                        throw new InvalidOperationException("PowerShell script execution failed: " + error);
                }

                File.Delete(tempScript);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to run PowerShell 7 script: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Runs the given PowerShell 5 script (Windows PowerShell, powershell.exe).
        /// </summary>
        /// <param name="script">PowerShell script to execute.</param>
        public void RunWithPwsh5(string script)
        {
            string powershellPath = "powershell.exe";
            try
            {
                // Check if powershell.exe is available
                ProcessStartInfo checkInfo = new ProcessStartInfo
                {
                    FileName = powershellPath,
                    Arguments = "-Command \"Write-Output 'PowerShell 5 check'\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var checkProcess = Process.Start(checkInfo))
                {
                    string output = checkProcess.StandardOutput.ReadToEnd();
                    string error = checkProcess.StandardError.ReadToEnd();
                    checkProcess.WaitForExit();

                    if (checkProcess.ExitCode != 0)
                        throw new InvalidOperationException("Windows PowerShell (powershell.exe) is not available in PATH.");
                }

                // Run the actual script
                string tempScript = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".ps1");
                File.WriteAllText(tempScript, script);

                ProcessStartInfo runInfo = new ProcessStartInfo
                {
                    FileName = powershellPath,
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{tempScript}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var runProcess = Process.Start(runInfo))
                {
                    string result = runProcess.StandardOutput.ReadToEnd();
                    string error = runProcess.StandardError.ReadToEnd();
                    runProcess.WaitForExit();

                    if (runProcess.ExitCode != 0)
                        throw new InvalidOperationException("PowerShell 5 script execution failed: " + error);
                }

                File.Delete(tempScript);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to run PowerShell 5 script: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Runs an inline PowerShell 7 script using a C#-asm-like syntax block.
        /// </summary>
        public void RunWithPwsh7()
        {
            // asm-pwsh {
            string script = @"
                    Get-Location
                    Write-Output 'This is PowerShell 7 inline code.'
                ";
            // }

            RunPwshWith7(script);
        }
    }
}
