using System.Transactions;
using Unity.Netcode;
using UnityEngine;

namespace ProjectTetrad.PlayerFeatures
{
    public class PlayerSpawnController : NetworkBehaviour
    {
        [SerializeField] private GameObject heroPrefab; // Your Hero_Base
        [SerializeField] private Transform travelerSpawnPoint;

        public override void OnNetworkSpawn()
        {
            // Only the Server/Host handles the actual spawning of objects
            if (!IsServer) return;

            // Subscribe to when new players join
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }

        private void OnClientConnected(ulong clientId)
        {
            // When someone else joins, spawn them at the gate
            SpawnPlayer(clientId);
        }

        private void SpawnPlayer(ulong clientId)
        {
            Vector3 spawnPos = travelerSpawnPoint.position;
            GameObject go = Instantiate(heroPrefab, spawnPos, Quaternion.identity);

            // This is the "Magic" line that links the object to the player's ID
            go.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
    }
}
