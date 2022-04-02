using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public static bool IsPaused = false;
        public Game()
        {
            InputService = new InputService();
        }
    }
}