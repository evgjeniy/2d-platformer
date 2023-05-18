namespace InputScripts
{
    public enum PlayerInputType
    {
        FirstPlayer,
        SecondPlayer
    }

    public static class PlayerInputTypeExtensions
    {
        public static string GetSaveKey(this PlayerInputType inputType) => inputType switch
        {
            PlayerInputType.FirstPlayer => "FirstPlayerSaveKey",
            PlayerInputType.SecondPlayer => "SecondPlayerSaveKey",
            _ => throw new System.ArgumentOutOfRangeException(nameof(inputType), inputType, null)
        };
    }
}