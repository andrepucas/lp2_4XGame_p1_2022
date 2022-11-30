public interface IUserInterface
{
    public void Initialize();
    
    public void ChangeUIState(UIStates p_uiState);

    public void UpdateAnalyticsData(int p_index, MapData p_mapData);
}
