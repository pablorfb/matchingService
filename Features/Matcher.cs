
using System.Collections.Generic;
using System.Collections.Concurrent;
using MatchingService.Models;

namespace MatchingService.Feature {

    public static class MatcherFactory
    {
        private static readonly Matcher Matcher = new Matcher();


        public static Matcher GetInstance()
        {
            return MatcherFactory.Matcher;
        }
    }

    public class Matcher {

        private IProducerConsumerCollection<GameRequest> _queue = new ConcurrentQueue<GameRequest>();
        private IDictionary<string, byte> _usersInQueue = new ConcurrentDictionary<string, byte>();

        internal Matcher() {}

        public bool TryAdd(GameRequest gameRequest)
        {
            if (!_usersInQueue.ContainsKey(gameRequest.UserId) && _queue.TryAdd(gameRequest))
            {
                _usersInQueue.Add(gameRequest.UserId, 0);
                return true;
            }

            return false;
        }

        public GameMatch TryMatch()
        {
            GameRequest request1 = null, request2 = null;

            if (_queue.Count >= 2 && _queue.TryTake(out request1) && _queue.TryTake(out request2))
            {
                _usersInQueue.Remove(request1.UserId);
                _usersInQueue.Remove(request2.UserId);
                return new GameMatch(new GameRequest[] { request1, request2 });
            }

            if (request1 != null)
            {
                _queue.TryAdd(request1);
            }

            return null;
                
        }

    }
}