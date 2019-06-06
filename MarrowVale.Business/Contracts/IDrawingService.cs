using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface IDrawingService
    {
        void PrintArtCentered(string[] art);
        void PrintArt(string[] art);
    }
}
