using UnityEngine;
using UnityEngine.InputSystem;

public class EnvironmentTestInput : MonoBehaviour
{
    [SerializeField] private EnvironmentManager environmentManager;

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            environmentManager.LoadEnvironment("Env_Hospital");
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            environmentManager.LoadEnvironment("Env_Classroom");
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            environmentManager.LoadEnvironment("Env_Office");
        }
    }
}