using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class LevelDemoController : Controller
{
    public const string LEVELDEMO_SCENE_NAME = "LevelDemo";

    public override string SceneName()
    {
        return LEVELDEMO_SCENE_NAME;
    }
}