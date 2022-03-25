using UnityEngine;

public class SlimeBody : MonoBehaviour
{
    [SerializeField] private float intensity = 1f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float stiffness = 1f;
    [SerializeField] private float damping = 0.75f;

    private Mesh originalMesh, meshClone;
    private MeshRenderer render;
    private SlimeVertex[] sv;
    private Vector3[] vertexArray;

    void Start()
    {
        gameObject.tag = "Slime";
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = meshClone;
        render = GetComponent<MeshRenderer>();
        sv = new SlimeVertex[meshClone.vertices.Length];
        for (int i = 0; i < meshClone.vertices.Length; i++)
        {
            sv[i] = new SlimeVertex(i, transform.TransformPoint(meshClone.vertices[i]));
        }

    }

    void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;
        for (int i = 0; i < sv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[sv[i].ID]);
            float intensity = (1 - (render.bounds.max.y - target.y) / render.bounds.size.y) * this.intensity;
            sv[i].Shake(target, mass, stiffness, damping);
            target = transform.InverseTransformPoint(sv[i].Position);
            vertexArray[sv[i].ID] = Vector3.Lerp(vertexArray[sv[i].ID], target, intensity);

        }
        meshClone.vertices = vertexArray;
    }

    public class SlimeVertex
    {
        public int ID;
        public Vector3 Position;
        public Vector3 velocity, Force;

        public SlimeVertex(int _id, Vector3 _pos)
        {
            ID = _id;
            Position = _pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            Force = (target - Position) * s;
            velocity = (velocity + Force / m) * d;
            Position += velocity;
            if ((velocity + Force + Force / m).magnitude < 0.001f)
            {
                Position = target;
            }
        }

    }
}
