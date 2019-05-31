using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Contracts
{
    public interface IGlobalItemsProvider
    {
        /// <summary>
        /// This will be used to house global properties
        /// </summary>
        int WindowWidth { get; }
    }
}
