using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Data.Contracts
{
    public interface IDrawingRepository
    {
        string[] GetTitleArt();
        void PrintArtCentered(string[] art);
        void PrintArt(string[] art);
    }
}
