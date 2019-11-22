using System;
using System.Collections.Generic;
using System.Text;

namespace Soundboard {
    class DisposableList<T> : List<T>, IDisposable where T : IDisposable {
        public void Dispose() {
            foreach (var item in this) {
                item.Dispose();
            }
        }
    }
}
