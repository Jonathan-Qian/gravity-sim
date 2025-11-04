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
        CalculateNetForces();
        ApplyNetForces();
    }

    private void CalculateNetForces() {
        float distance;
        Vector3 force, distanceComponents;
        Transform mass1, mass2;
        MassData massData1, massData2;

        for (int i = 0; i < numMasses; i++) {
            mass1 = transform.GetChild(i);

            for (int j = i + 1; j < numMasses; j++) {
                mass2 = transform.GetChild(j);

                distanceComponents = mass2.position - mass1.position;

                distance = Mathf.Sqrt(
                    distanceComponents.x * distanceComponents.x +
                    distanceComponents.y * distanceComponents.y +
                    distanceComponents.z * distanceComponents.z
                );

                massData1 = mass1.GetComponent<MassData>();
                massData2 = mass2.GetComponent<MassData>();

                force = distanceComponents * (gravitationalConstant * massData1.mass * massData2.mass / (distance * distance * distance));

                netForces[i] += force;
                netForces[j] -= force;
            }
        }
    }

    private void ApplyNetForces() {
        MassData massData;

        for (int i = 0; i < numMasses; i++) {
            massData = transform.GetChild(i).GetComponent<MassData>();
            massData.MoveWithForce(netForces[i]);
            netForces[i] = Vector3.zero;
        }
    }
}
