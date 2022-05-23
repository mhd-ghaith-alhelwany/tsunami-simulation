using Models;
using System.Collections.Generic;
namespace Generators{
    public abstract class Generator
    {
        public abstract List<Particle> start(List<Particle> existingParticles);
        public abstract List<Particle> update(List<Particle> existingParticles);
    }
}