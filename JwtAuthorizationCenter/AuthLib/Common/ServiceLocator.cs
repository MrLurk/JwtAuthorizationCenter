using System;
using System.Collections.Generic;
using System.Text;

namespace AuthLib.Common {

    public static class ServiceLocator {
        public static IServiceProvider Instance { get; set; }
    }
}
