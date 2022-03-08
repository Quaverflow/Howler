using System;

namespace Howler.Tests.Objects.StructureExamples;

public class AddToDbEventArgs : EventArgs
{
    public AddToDbEventArgs(string hello)
    {
        Hello = hello;
    }

    public string Hello { get; set; }
}