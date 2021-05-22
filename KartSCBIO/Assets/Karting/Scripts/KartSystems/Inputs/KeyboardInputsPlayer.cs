using UnityEngine;

namespace KartGame.KartSystems 
{
    public class KeyboardInputsPlayer : BaseInput
    {
        private ReadPythonSocket pythonSocket;
        private void Awake() {
            pythonSocket = FindObjectOfType<ReadPythonSocket>();
        }
        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = pythonSocket.acelerar,
                Brake = pythonSocket.frenar,
                TurnInput = pythonSocket.giro,
                Throw = pythonSocket.objeto
            };
        }
    }
}
