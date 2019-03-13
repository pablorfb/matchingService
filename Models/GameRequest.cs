
using System.ComponentModel.DataAnnotations;

namespace MatchingService.Models {
    public class GameRequest
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string UserId { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string GameId { get; set; }

    }

    public class GameMatch
    {
        public GameRequest[] GameRequests { get; private set; }

        public GameMatch(GameRequest[] gameRequests)
        {
            this.GameRequests = gameRequests;
        }

    }
}