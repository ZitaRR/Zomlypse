using System;

namespace Zomlypse.Interfaces
{
    public interface IPrompt
    {
        bool Success { get; set; }
        Action<IPrompt> OnInput { get; }
    }
}
