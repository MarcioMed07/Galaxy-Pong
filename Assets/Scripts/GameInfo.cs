
public static class GameInfo
{
    public static GameMode gameMode { get; set; }
}


public enum GameMode
{
    Player = 0,
    ComputerEasy = 1,
    ComputerMedium = 2,
    ComputerHard = 3
}