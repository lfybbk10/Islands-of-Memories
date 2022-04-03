using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        private static bool _isPaused;
        public static bool IsPaused { get => _isPaused;
            set
            {
                if (value)
                {
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.visible = false;
                }
                _isPaused = value;
            }
        }
        public Game()
        {
            Cursor.visible = false;
            InputService = new InputService();
        }
    }
}