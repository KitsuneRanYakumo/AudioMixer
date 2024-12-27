public static class Converter
{
    public static string EnumToString(ExposedParameters exposedParameter)
    {
        return exposedParameter switch
        {
            ExposedParameters.MasterVolume => "MasterVolume",
            ExposedParameters.SoundVolume => "SoundVolume",
            ExposedParameters.BackgroundSoundVolume => "BackgroundSoundVolume",
            _ => null,
        };
    }
}