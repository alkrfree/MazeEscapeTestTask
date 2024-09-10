using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Services.Input
{
  public interface IInputService 
  {
    Vector2 Axis { get; }
  }
}