using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.WordFiller.FillerStatus
{
    public static class StrategyFactory
    {
        public static IFillerStatusStrategy CreateStrategy(WordFillerOptions options)
        {
            return options.IsBackground ? (IFillerStatusStrategy) new BackgroundStrategy() : new FormStrategy();
        }
    }
}
