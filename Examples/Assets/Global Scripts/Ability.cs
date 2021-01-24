[System.Serializable]
public class Ability : IPowerUp
{
    public string Type;
    public float Value;

    private AbilityModifer modifer;

    #region Class Logic
    public void ChangeValueDueSeconds(float changeValue, int time) => DoTimer(changeValue, time);

    private async void DoTimer(float changeValue, int time)
    {
        modifer = new AbilityModifer();
        await modifer.SetTemporalValue(this, changeValue, time);

        modifer.Dispose();
    }
    #endregion
}