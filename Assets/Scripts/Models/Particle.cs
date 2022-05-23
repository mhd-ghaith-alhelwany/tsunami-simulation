using UnityEngine;

namespace Models{
    public class Particle : Model
    {
        public float density, pressure;
        public Vector2 force, velocity;
        public GameObject prefab;
        public GameObject gameObject;

        public Particle(Vector2 position, GameObject prefab): base(position)
        {
            this.force = new Vector2(0.0f, 0.0f);
            this.velocity = new Vector2(0.0f, 0.0f);
            this.density = 0.0f;
            this.pressure = 0.0f;
            this.prefab = prefab;
            this.render();
        }

        override
        public GameObject getPrefab()
        {
            return this.prefab;
        }
    }
}