using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs: EventArgs{
        public float progressNormalized;
    }
}

