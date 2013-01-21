using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisaCzech.BL.Background
{
    public static class StrategyFactory
    {
        public static IBackgroundStrategy CreateStrategy(bool isBackground)
        {
            return isBackground ? (IBackgroundStrategy) new BackgroundStrategy() : new FormStrategy();
        }
    }
}
