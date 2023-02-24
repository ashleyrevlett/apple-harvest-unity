using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void LoadSequinScene() {
        SceneManager.LoadScene("Sequins");
    }

    public void LoadLeavesScene() {
        SceneManager.LoadScene("Leaves");
    }

    public void LoadCubeLookScene() {
        SceneManager.LoadScene("CubeLook");
    }

    public void LoadCubeFlipScene() {
        SceneManager.LoadScene("CubeFlip");
    }
}
