using UnityEngine;
namespace Models
{
    public abstract class Model
    {
        private Vector2 position;
        private GameObject gameObject;

        public Model(Vector2 position)
        {
            this.position = position;
        }

        public void render()
        {
            this.gameObject = UnityEngine.Object.Instantiate(this.getPrefab(), this.position, Quaternion.identity);
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
            this.updatePosition();
        }

        public void updatePosition()
        {
            this.gameObject.transform.localPosition = this.position;
        }

        public abstract GameObject getPrefab();
    }
}