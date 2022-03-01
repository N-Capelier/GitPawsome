using UnityEngine;

namespace UTPI.Clocks
{
    public class UpdateCaller : MonoBehaviour
    {
        public static UpdateCaller Instance = null;

        //Event used to run the clocks (UTPI Clocks package)
        public delegate void UpdateCall();
        public static event UpdateCall OnUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}