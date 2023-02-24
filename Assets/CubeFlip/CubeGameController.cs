using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CubeGameController : MonoBehaviour
{
    public float thrust = -2f;
    public float torque = 2f;
    CubeGridManager grid;
    bool hasWon = false;
    Vector3 cameraTargetPosition;
    float cameraSpeed = 20f;

    void Start() {
        grid = gameObject.GetComponent<CubeGridManager>();
        // cameraTargetPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - 3f);

        Physics.gravity = new Vector3(0, 0f, 18f);
    }

    bool CheckWinCondition() {
        List<GameObject> items = grid.GetItems();
        // Debug.Log(items.Count);
        GameObject firstItem = items.First();
        DieController firstDie = firstItem.GetComponent<DieController>();
        int firstFaceUpSide = firstDie.GetFaceUpSide();
        // Debug.Log($"{firstItem.name} up side: {firstFaceUpTransform}");

        // Debug.Log($"first FaceUpTransform: {firstFaceUpTransform}");
        if (items.Count <= 0 || firstFaceUpSide == 0) {
          return false;
        }

        for (int i = 1; i < items.Count; i++) {
            // Debug.Log($"i: {i}");
            GameObject item = items[i];
            DieController die = item.GetComponent<DieController>();
            int faceUpSide = die.GetFaceUpSide();
            if (faceUpSide == 0) {
              // die is still moving
              return false;
            }
            // Debug.Log($"{item.name} up side: {faceUpTransform}");
            if (faceUpSide != firstFaceUpSide) {
                return false;
            }
        }

        return true;
    }

    void Update() {
        // Debug.Log(items);
        bool didWin = CheckWinCondition();
        if (didWin && !hasWon) {
          DoWinEffect();
        }
        Debug.Log($"didWin: {didWin}");

        // if (hasWon && Camera.main.transform.position != cameraTargetPosition) {
        //     var step = cameraSpeed * Time.deltaTime; // calculate distance to move
        //     Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraTargetPosition, step);
        // }
    }

    void DoWinEffect() {
        hasWon = true;
        List<GameObject> items = grid.GetItems();

        for (int i = 0; i < items.Count; i++) {
            GameObject obj = items[i];
            Rigidbody body = obj.GetComponent<Rigidbody>();
            BoxCollider collider = obj.GetComponent<BoxCollider>();
            body.useGravity = true;
            collider.isTrigger = false;

            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            body.AddTorque(new Vector3(randomX, randomY, 0f) * torque);
            body.AddForce(0, 0, thrust, ForceMode.Impulse);
        }
    }

    public void RestartScene() {
        SceneManager.LoadScene("CubeFlip");
    }
}
