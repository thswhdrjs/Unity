using UnityEngine;
using Microsoft.Win32;
using System.Security;
using System.Security.Permissions;
using System.ServiceProcess;

public class Regedit : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PermissionSet ps1 = new PermissionSet(PermissionState.None);

        ps1.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
        ps1.PermitOnly();

        try
        {
            RegistryKey parametersKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WebClient\Parameters", true);

            parametersKey.SetValue("BasicAuthLevel", 2, RegistryValueKind.DWord);
            parametersKey.SetValue("FileSizeLimitInBytes", 4294967295, RegistryValueKind.QWord);
        }
        catch (SecurityException e)
        {
            print("Security Exception: " + e.Message);
        }

        ServiceController sc = new ServiceController("WebClient");

        if (sc.Status == ServiceControllerStatus.Running)
        {
            sc.Stop();
            sc.WaitForStatus(ServiceControllerStatus.Stopped);

            sc.Start();
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }
        else
        {
            sc.Start();
            sc.WaitForStatus(ServiceControllerStatus.Running);
        }

        Connect.checkRegit = true;
    }
}