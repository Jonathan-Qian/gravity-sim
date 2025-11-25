using UnityEngine;

public class MassSystem : MonoBehaviour {
    [SerializeField] private float gravitationalConstant;
    private int numMasses;
    private Vector3[] netForces;

    void Start() {
        numMasses = transform.childCount;
        netForces = new Vector3[numMasses];
    }

    void FixedUpdate() {
        CalculateAndApplyNetForces();
    }

    private void CalculateAndApplyNetForces() {
        float distance;
        Vector3 force, distanceComponents;
        Transform mass1, mass2;
        MassData massData1, massData2;

        // for each pair i, j where 0 <= i < j < numMasses
        // according to the definition of i and j above, the
        // outer loop should go from i = 0 to i = numMasses - 2,  
        // but in order to call MoveWithForce() on the last
        // element, we will go from i = 0 to i = numMasses - 1
        for (int i = 0; i < numMasses; i++) {
            mass1 = transform.GetChild(i);
            massData1 = mass1.GetComponent<MassData>();

            for (int j = i + 1; j < numMasses; j++) {
                mass2 = transform.GetChild(j);
                massData2 = mass2.GetComponent<MassData>();

                distanceComponents = mass2.position - mass1.position;
                distance = Mathf.Sqrt(
                    distanceComponents.x * distanceComponents.x +
                    distanceComponents.y * distanceComponents.y +
                    distanceComponents.z * distanceComponents.z
                );

                // F_x,y,z = (d_x,y,z / d) * (G * m1 * m2 / d^2) = d_x,y,z * G * m1 * m2 / d^3
                force = distanceComponents * gravitationalConstant * massData1.mass * massData2.mass / (distance * distance * distance);

                netForces[i] += force;
                netForces[j] -= force;
            }

            massData1.MoveWithForce(netForces[i]);
            netForces[i] = Vector3.zero; // clear net force for next iteration
        }
    }
}
