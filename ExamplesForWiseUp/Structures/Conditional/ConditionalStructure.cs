using Howler;

namespace ExamplesForWiseUp.Structures.Conditional;

public class ConditionalStructure : IHowlerStructure
{
    public async Task NotifyIfChris(Func<Task> method, string name)
    {
        if (name.Equals("Chris", StringComparison.InvariantCultureIgnoreCase))
        {
            await method.Invoke();
        }
    }

    public void InvokeRegistrations()
    {
        HowlerRegistry.AddStructure(StructureIds.NotifyItIsChris, NotifyIfChris);
    }
}