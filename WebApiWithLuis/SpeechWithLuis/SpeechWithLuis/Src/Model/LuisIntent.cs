﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeechWithLuis.Src.Model
{
    public class LuisIntent
    {
        public string Intents { get; set; }

        public float score { get; set; }
    }
}