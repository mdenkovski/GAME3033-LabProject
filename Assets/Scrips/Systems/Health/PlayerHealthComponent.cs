using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : HealthComponent
{



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.Invoke_OnHealthInitialized(this);
    }

    public override void Destroy()
    {
        base.Destroy();
        SceneManager.LoadScene("Main Menu");
    }
}
