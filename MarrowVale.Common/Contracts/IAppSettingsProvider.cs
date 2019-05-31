using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Contracts
{
    public interface IAppSettingsProvider
    {
        string DataFilesLocation { get; }
        string SoundFilesLocation { get; }
    }
}
