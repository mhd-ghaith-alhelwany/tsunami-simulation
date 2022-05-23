using UnityEngine;
using Main;
using Models;
using System.Collections.Generic;

namespace Generators{
    public class BucketGenerator: Generator
    {
        private Game game;
        private Vector2 gridSize;
        private Vector2 startingPoint;

        public BucketGenerator(Game game, Vector2 gridSize, Vector2 startingPoint) : base()
        {
            this.game = game;
            this.gridSize = gridSize;
            this.startingPoint = startingPoint;
            
        }

        override
        public List<Particle> start(List<Particle> existingParticles)
        {
            Vector2 boxSize = this.game.getBoxSize();
            float particleSize = this.game.getParticleSize();

            for(int i = 0; i < this.gridSize[0]; i++){
                float rand = 0;
                for(int j = 0; j < this.gridSize[1]; j++){
                    rand += getRand();
                    existingParticles.Add(
                        new Particle(
                            new Vector2(
                                j * particleSize + startingPoint[1] + rand,
                                i * particleSize + startingPoint[0] 
                            ), 
                            game.getFluidPrefab()
                        )
                    );
                }
            }
            return existingParticles;
        }

        private float getRand()
        {
            return new System.Random().Next((int)this.game.getParticleSize() / 4);
        }

        override
        public List<Particle> update(List<Particle> existingParticles)
        {
            return existingParticles;
        }
    }
}