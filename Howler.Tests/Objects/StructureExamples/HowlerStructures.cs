using System;

namespace Howler.Tests.Objects.StructureExamples;

public static class HowlerStructures
{
    public static event EventHandler? SayHello;
    public static event EventHandler<AddToDbEventArgs>? AddToDb;


    private static bool _registered;

    public static void RegisterTryCatchStructureShape()
    {
        HowlerRegistration.AddStructure(StructuresIds.TryCatchStructureId, x =>
        {
            try
            {
                return x.DynamicInvoke();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("uh-oh" + e.Message);
            }
        });
    }

    private static void OnSayHello(EventArgs e) => SayHello?.Invoke(null, e);
    private static void OnAddToDb(AddToDbEventArgs e) => AddToDb?.Invoke(null, e);

    public static void RegisterOnSayHelloShape()
    {
        HowlerRegistration.AddStructure(StructuresIds.SayHelloRaisingStructureId, x =>
        {
            OnSayHello(EventArgs.Empty);
            return x.DynamicInvoke();
        });
    }
    public static void RegisterOnAddToDbShape()
    {
        HowlerRegistration.AddStructure(StructuresIds.AddToDbRaisingStructureId, x =>
        {
            OnAddToDb(new AddToDbEventArgs("Hello!"));
            return x.DynamicInvoke();
        });
    }

    public static void AllStructures()
    {
        if (_registered)
        {
            return;
        }
        _registered = true;
        RegisterOnSayHelloShape();
        RegisterTryCatchStructureShape();
        RegisterOnAddToDbShape();

    }
}