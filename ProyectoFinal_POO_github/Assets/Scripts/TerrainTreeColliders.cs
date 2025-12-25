using UnityEngine;
using UnityEngine.AI;

public class TerrainTreeColliders : MonoBehaviour
{
    public Terrain terrain;    // terreno de juego
    public float treeColliderHeight = 5f; // Altura aproximada del árbol
    public float treeColliderRadius = 0.5f; // Radio aproximado

    // Nombres de los árboles que queremos colisionables
    public string[] colliderTreeNames = { "PT_Fruit_Tree_01_dead", "PT_Pine_Tree_03_green", "PT_Fruit_Tree_01_apples",  "PT_Pine_Tree_03_dead" };

    void Start()
    {
        if (terrain == null) return;

        TerrainData terrainData = terrain.terrainData;
        TreePrototype[] prototypes = terrainData.treePrototypes;




        

        // Obtener los índices de los prototipos que queremos colisionables
        System.Collections.Generic.List<int> colliderIndices = new System.Collections.Generic.List<int>();
        for (int i = 0; i < prototypes.Length; i++)
        {
            foreach (string name in colliderTreeNames)
            {
                if (prototypes[i].prefab.name == name)
                {
                    colliderIndices.Add(i);
                    break;
                }
            }
        }

        // Crear colliders solo para los árboles que coinciden
        foreach (TreeInstance tree in terrainData.treeInstances)
        {
            if (!colliderIndices.Contains(tree.prototypeIndex))
                continue;

            Vector3 treeWorldPos = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            GameObject colliderObj = new GameObject("TreeCollider");
            colliderObj.transform.position = treeWorldPos + new Vector3(0, treeColliderHeight / 2f, 0);
            colliderObj.transform.parent = terrain.transform;

            // Collider para jugador
            CapsuleCollider capsule = colliderObj.AddComponent<CapsuleCollider>();
            capsule.height = treeColliderHeight;
            capsule.radius = treeColliderRadius;
            capsule.center = Vector3.zero;
            
            // Obstacle para enemigos
            NavMeshObstacle obstacle = colliderObj.AddComponent<NavMeshObstacle>();
            obstacle.shape = NavMeshObstacleShape.Capsule;
            obstacle.radius = treeColliderRadius;
            obstacle.height = treeColliderHeight;
            obstacle.carving = true;  // importante para que los agentes lo eviten
        }
    }
}