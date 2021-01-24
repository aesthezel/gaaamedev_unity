using System;
using System.Threading.Tasks;

public class AbilityModifer 
{
    public async Task SetTemporalValue(Ability ability, float changeValue, int time)
    {
        float realValue = ability.Value;
        ability.Value = changeValue;
        await Task.Delay(time);

        ability.Value = realValue;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}