using UnityEngine;

namespace KartGame.KartSystems 
{
    public class KeyboardInputsPlayer : BaseInput
    {
        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = Input.GetButtonDown("Accelerate"),
                Brake = Input.GetButtonDown("Brake"),
                TurnInput = Input.GetAxis("Horizontal")
            };
        }
    }
}
