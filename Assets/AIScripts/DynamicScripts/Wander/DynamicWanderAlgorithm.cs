using AIEngine.Movement.Agents;
using AIEngine.Movement.Output;
using System;
using System.Numerics;

namespace AIEngine.Movement.Algorithms
{
    public class DynamicWanderAlgorithm : DynamicFaceAlgorithm
    {
        private AgKinematic agent;

        private float wanderDistance;
        private float wanderRadius;
        private float maxOrientationRatio;
        private float targetActualOrientation;
        private float maxAcceleration;

        public DynamicWanderAlgorithm(
            float maxAngularAcceleration, float maxRotation, float targetRange, float decelerationRange, float timeToTarget,
            float wanderDistance, float wanderRadius, float maxOrientationRatio, float maxAcceleration)
            : base(maxAngularAcceleration, maxRotation, targetRange, decelerationRange, timeToTarget)
        {
            this.wanderDistance = wanderDistance;
            this.wanderRadius = wanderRadius;
            this.maxOrientationRatio = maxOrientationRatio;
            this.maxAcceleration = maxAcceleration;
        }

        public new void SetAgent(AgKinematic agent)
        {
            this.agent = agent;
            base.SetAgent(agent); // Muy importante para evitar null en base
        }

        public override SteeringOutput GetSteering()
        {
            // Actualiza la orientaci�n aleatoria
            targetActualOrientation += UnityEngine.Random.Range(-1f, 1f) * maxOrientationRatio;

            // Calcula la orientaci�n objetivo sum�ndola a la del agente
            float targetOrientation = targetActualOrientation + agent.orientation;

            // Calcula el punto central del c�rculo de vagabundeo frente al agente
            Vector2 direction = OrientationToVector(agent.orientation);
            Vector2 circleCenter = agent.position + wanderDistance * direction;


            // Calcula el offset sobre el borde del c�rculo en la direcci�n del objetivo
            Vector2 offset = wanderRadius * OrientationToVector(targetOrientation);

            // Posici�n objetivo de vagabundeo
            Vector2 wanderTargetPosition = circleCenter + offset;

            // Crea un objetivo cinem�tico hacia el que el agente va a "facear"
            AgKinematic wanderTarget = new AgKinematic(wanderTargetPosition, targetOrientation, Vector2.Zero, 0f);
            base.SetTarget(wanderTarget);

            // Calcula la respuesta angular desde Face
            SteeringOutput steering = base.GetSteering();

            // Calcula la respuesta lineal hacia delante en base a la orientaci�n del agente
            steering.linear = maxAcceleration * direction;

            return steering;
        }

        private Vector2 OrientationToVector(float orientation)
        {
            return new Vector2(MathF.Sin(orientation), MathF.Cos(orientation));
        }
    }
}
