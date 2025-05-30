﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public interface IMarkdownSource
    {
        string GetMarkdownText();
    }

    public interface IMarkdownTarget
    {
        void ShowHtml(string html);
    }
}
