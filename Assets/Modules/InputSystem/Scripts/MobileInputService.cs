using UnityEngine;

namespace Modules.InputSystem.Scripts
{
  public class MobileInputService : InputService
  {
    public override Vector2 Axis => SimpleInputAxis();
  }
}