using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldSteponPlayer : MonoBehaviour
{

    public AudioSource steppiesAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Called from the kobold animation itself
    [UnityEngine.Scripting.Preserve]
    void animStep()
    {
        Debug.LogError("Something has gone terribly wrong. The kobold no longer steppies");
        steppiesAudio.PlayOneShot(steppiesAudio.clip);
    }
}
