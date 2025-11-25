using UnityEngine;

public class MassData : MonoBehaviour {
    public float mass;
    public Vector3 velocity;
    public float radius;
    private float timeMassRatio;

    void Start() {
        SetRadius(radius);
        timeMassRatio = Time.fixedDeltaTime / mass;
    }

    void FixedUpdate() {
        SetDirection(velocity);
    }

    private void SetRadius(float radius) {
        transform.localScale = new Vector3(radius, radius, radius);
    }

    public void MoveWithForce(Vector3 netForce) {
        velocity += netForce * timeMassRatio;
        transform.position += velocity * Time.fixedDeltaTime;
    }

    private void SetDirection(Vector3 velocity) {
        if (!velocity.Equals(Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(velocity.normalized);
        }
    }
}
