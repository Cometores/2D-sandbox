using System;

namespace FlappyBird
{
    public sealed class VolumeChangedEventArgs : EventArgs
    {
        public float OldVolume { get; }
        public float NewVolume { get; }

        public VolumeChangedEventArgs(float oldVolume, float newVolume)
        {
            OldVolume = oldVolume;
            NewVolume = newVolume;
        }
    }
}