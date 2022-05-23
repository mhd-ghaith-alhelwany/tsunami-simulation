using Main;
namespace Controllers
{
    public abstract class Controller
    {
        protected Game game;
        public Controller(Game game)
        {
            this.game = game;
        }
        public abstract void start();
        public abstract void update();
    }
}