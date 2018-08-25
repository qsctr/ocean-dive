using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fish : MonoBehaviour {

    public class Settings {
        public float maxSpeed { get; set; }
        public float maxForce { get; set; }
        public float separationRadius { get; set; }
        public float neighborRadius { get; set; }
        public float separationWeight { get; set; }
        public float alignmentWeight { get; set; }
        public float cohesionWeight { get; set; }
    }

    public Settings settings { get; set; }
    public FishType type { get; set; }

    const float speedMultiplier = 1;

    public Vector2 position { get; private set; }
    public Vector2 velocity { get; private set; }
    Vector2 newPosition;
    Vector2 newVelocity;

    void Start() {
        position = new Vector2(transform.position.x, transform.position.z);
        velocity = Vector2.zero;
    }

    float DistanceTo(Fish other) => (other.position - position).magnitude;

    Vector2 Separation(IEnumerable<Fish> others) => Average(
        from other in others where DistanceTo(other) < settings.separationRadius
        select (position - other.position).normalized / DistanceTo(other));

    Vector2 Alignment(IEnumerable<Fish> others) =>
        Vector2.ClampMagnitude(Average(
            from other in others
            where DistanceTo(other) < settings.neighborRadius
            select other.velocity), settings.maxForce);

    Vector2 Cohesion(IEnumerable<Fish> others) =>
        Vector2.ClampMagnitude(Average(
            from other in others
            where DistanceTo(other) < settings.neighborRadius
            select other.position - position), settings.maxForce);

    public void Calculate(IEnumerable<Fish> flock, float halfWidth) {
        var others = flock.Where(other => other != this);
        newVelocity = Vector2.ClampMagnitude(velocity
            + Separation(others) * settings.separationWeight
            + Alignment(others) * settings.alignmentWeight
            + Cohesion(others) * settings.cohesionWeight,
            settings.maxSpeed) * speedMultiplier;
        newPosition = position + newVelocity * Time.deltaTime;
        newPosition.Set(
            wrap(newPosition.x, halfWidth),
            wrap(newPosition.y, halfWidth)
        );
    }

    public void Write() {
        transform.position =
            new Vector3(newPosition.x, transform.position.y, newPosition.y);
        var angle = 180 - Vector2.SignedAngle(Vector2.right, newVelocity);
        transform.eulerAngles = new Vector3(0, angle, 0);
        position = newPosition;
        velocity = newVelocity;
    }

    static float wrap(float n, float halfWidth) =>
        Mathf.Repeat(n + halfWidth, halfWidth * 2) - halfWidth;

    static Vector2 Average(IEnumerable<Vector2> vectors) =>
        vectors.Any()
            ? vectors.Aggregate(Vector2.zero, (x, y) => x + y) / vectors.Count()
            : Vector2.zero;

}
