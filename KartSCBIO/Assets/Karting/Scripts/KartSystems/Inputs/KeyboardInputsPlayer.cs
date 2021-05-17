using UnityEngine;

namespace KartGame.KartSystems 
{
    public class KeyboardInputsPlayer : BaseInput
    {
        public readMatlabSocket matlabSocket;

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = matlabSocket.acelerar,
                Brake = matlabSocket.frenar,
                TurnInput = Input.GetAxis("Horizontal")
            };
        }
    }
}
