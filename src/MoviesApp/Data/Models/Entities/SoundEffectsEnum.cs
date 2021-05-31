using System;

namespace MoviesApp.Data.Models.Entities
{
    [Flags]
    public enum SoundEffectsEnum
    {
        None = 0,
        RX6 = 1,
        SDDS = 2,
        DOLBY = 4,
        DTS = 8
    }
}
