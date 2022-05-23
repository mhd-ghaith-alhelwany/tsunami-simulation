using UnityEngine;
using Generators;
using Models;
using  Controllers;

using System.Collections.Generic;

namespace Main {

    public class Game
    {
        private float particleSize;
        private Vector2 boxSize;
        private Vector2 gridSize;
        private Vector2 startingPoint;
        private GameObject fluidPrefab;
        private GameObject boxPrefab;

        private Generator[] generators;
        private Controller controller;

        private List<Particle> particles;
        private GameObject boxObject;
    
        public Game(GameObject fluidPrefab, GameObject boxPrefab)
        {
            this.particleSize = 16f;
            this.boxSize = new Vector2(500, 500);

            this.fluidPrefab = fluidPrefab;
            this.fluidPrefab.transform.localScale = new Vector2(this.particleSize, this.particleSize);

            this.boxPrefab = boxPrefab;
            this.boxPrefab.transform.localScale = boxSize;

            this.particles = new List<Particle>();;

            this.generators = new Generator[4];
            this.generators[0] = new BucketGenerator(this, new Vector2(10, 5), new Vector2(0, 50));
            this.generators[1] = new BucketGenerator(this, new Vector2(10, 5), new Vector2(0, -100));
            this.generators[2] = new FountainGenerator(this, 100, new Vector2(125, 20 + (-this.boxSize[1]/2)));
            this.generators[3] = new FountainGenerator(this, 100, new Vector2(-125, 20 + (-this.boxSize[1]/2)));

            this.controller = new SphController(this);
        }

        public Vector2 getBoxSize()
        {
            return this.boxSize;
        }

        public float getParticleSize()
        {
            return this.particleSize;
        }

        public List<Particle> getParticles()
        {
            return this.particles;
        }

        public GameObject getFluidPrefab()
        {
            return this.fluidPrefab;
        }

        public bool isOutsideBox(Vector2 point)
        {
            return 
                point[0] >= this.boxSize[0] / 2 || 
                point[1] >= this.boxSize[1] / 2 || 
                point[0] <= -1 * this.boxSize[0] / 2 || 
                point[1] <= -1 * this.boxSize[1] / 2;
        }

        public void start()
        {
            this.boxObject = UnityEngine.Object.Instantiate(this.boxPrefab, new Vector3(0, 0, 1), Quaternion.identity);
            this.particles = this.generators[0].start(this.getParticles());
            this.particles = this.generators[1].start(this.getParticles());
            this.particles = this.generators[2].start(this.getParticles());
            this.particles = this.generators[3].start(this.getParticles());
            this.controller.start();
        }

        public void update()
        {
            this.particles = this.generators[0].update(this.getParticles());
            this.particles = this.generators[1].update(this.getParticles());
            this.particles = this.generators[2].update(this.getParticles());
            this.particles = this.generators[3].update(this.getParticles());
            this.controller.update();
        }
    }
}