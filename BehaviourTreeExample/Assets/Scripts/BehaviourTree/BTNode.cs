using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
    public bool initialised = false;
    public enum BTResult { Success, Running, Failed }
    public abstract BTResult Run();
    public virtual void OnEnter() {
        initialised = true;
    }
    public virtual void OnExit() {
        initialised = false;
    }
}
