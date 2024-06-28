using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoSingelton<CoreGameSignals>
    {
        public UnityAction OnGameStart = delegate { };
        public UnityAction OnGameRestart = delegate { };
        public UnityAction OnGameOver = delegate { };
        public UnityAction OnCollectCoin = delegate { };
    }
}
