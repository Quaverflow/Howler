﻿// See https://aka.ms/new-console-template for more information

using HowlerExamples.Structures;

var counter = HumanCounter.GetSingleton();
counter.Subscribe(HumanObserverFactory.Observer);


while (true)
{
    Thread.Sleep(100);
    if (QueueSystem.HumansCreated.Count > 0)
    {
        Console.WriteLine(QueueSystem.HumansCreated.Dequeue());
    }
}
