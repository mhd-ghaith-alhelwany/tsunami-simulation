using UnityEngine;
using Main;
using Models;
using System.Collections.Generic;

namespace Generators
{
    public class FountainGenerator : Generator
    {

        private int numberOfParticles;
        private Vector2 source;
        private Game game;
        private int ticks, updateRate;

        public FountainGenerator(Game game, int numberOfParticles, Vector2 source)
        {
            this.game = game;
            this.numberOfParticles = numberOfParticles;
            this.source = source;
            this.ticks = 0;
            this.updateRate = 5;
        }

        override
        public List<Particle> start(List<Particle> existingParticles)
        {
            return existingParticles;
        }

        override
        public List<Particle> update(List<Particle> existingParticles)
        {
            if(this.numberOfParticles <= 0)
                return existingParticles;
            if(this.ticks != this.updateRate){
                this.ticks++;
                return existingParticles;
            }
            this.ticks = 0;
            this.numberOfParticles--;
            System.Random random = new System.Random();
            Particle p = new Particle(this.source, this.game.getFluidPrefab());
            p.velocity = new Vector2(
                random.Next(-20, 20),
                5
            );
            existingParticles.Add(p);
            return existingParticles;
        }
    }
}